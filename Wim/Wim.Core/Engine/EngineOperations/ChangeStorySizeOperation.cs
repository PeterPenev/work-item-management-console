using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStorySizeOperation : IEngineOperations
    {
        private const string StorySizeChanged = "Story {0} size is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IStoryOperations storyOperations;

        public ChangeStorySizeOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IStoryOperations storyOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.storyOperations = storyOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeStorySizeFor = inputParameters[0];
            string boardToChangeStorySizeFor = inputParameters[1];
            string storyToChangeSizeFor = inputParameters[2];
            string newStorySize = inputParameters[3];
            string authorOfStorySizeChange = inputParameters[4];

            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeSizeFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStorySizeFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStorySizeFor, boardTypeForChecking);

            var sizeTypeForChecking = "Size";
            inputValidator.IsNullOrEmpty(newStorySize, sizeTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStorySizeChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStorySizeFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor, storyToChangeSizeFor);

            //Operations
            var itemType = "Story";

            var newSizeEnum = enumParser.GetStorySize(newStorySize);

            var castedStoryForSizeChange = allTeams.FindStoryAndCast(teamToChangeStorySizeFor, boardToChangeStorySizeFor, storyToChangeSizeFor);

            castedStoryForSizeChange.ChangeStorySize(newSizeEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStorySizeFor, authorOfStorySizeChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStorySizeFor, itemType, boardToChangeStorySizeFor, storyToChangeSizeFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStorySizeFor, boardToChangeStorySizeFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStorySize);

            storyOperations.AddActivityHistoryToWorkItem(storyToAddActivityFor, memberToAddActivityFor, newStorySize);

            return string.Format(StorySizeChanged, storyToChangeSizeFor, newSizeEnum);
        }
    }
}
