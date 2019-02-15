using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStorySizeOperation
    {
        private const string StorySizeChanged = "Story {0} size is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public ChangeStorySizeOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
        }

        public string ChangeStorySize(string teamToChangeStorySizeFor, string boardToChangeStorySizeFor, string storyToChangeSizeFor, string newStorySize, string authorOfStorySizeChange)
        {
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

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            return string.Format(StorySizeChanged, storyToChangeSizeFor, newSizeEnum);
        }
    }
}
