using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStoryPriorityOperation : IEngineOperations
    {
        private const string StoryPriorityChanged = "Story {0} priority is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IStoryOperations storyOperations;
        private readonly IBusinessLogicValidator businessLogicValidator;

        public ChangeStoryPriorityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IStoryOperations storyOperations,
            IBusinessLogicValidator businessLogicValidator)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.storyOperations = storyOperations;
            this.businessLogicValidator = businessLogicValidator;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeStoryPriorityFor = inputParameters[0];
            string boardToChangeStoryPriorityFor = inputParameters[1];
            string storyToChangePriorityFor = inputParameters[2];
            string newStoryPriority = inputParameters[3];
            string authorOfStoryPriorityChange = inputParameters[4];
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangePriorityFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(newStoryPriority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryPriorityChange, authorTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToChangeStoryPriorityFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor);

            businessLogicValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor, storyToChangePriorityFor);

            //Operations
            var itemType = "Story";

            var newPriorityEnum = enumParser.GetPriority(newStoryPriority);

            var castedStoryForPriorityChange = allTeams.FindStoryAndCast(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            castedStoryForPriorityChange.ChangeStoryPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryPriorityFor, authorOfStoryPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryPriorityFor, itemType, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryPriority);

            storyOperations.AddActivityHistoryToWorkItem(storyToAddActivityFor, memberToAddActivityFor, newStoryPriority);

            return string.Format(StoryPriorityChanged, storyToChangePriorityFor, newPriorityEnum);

        }
    }
}
