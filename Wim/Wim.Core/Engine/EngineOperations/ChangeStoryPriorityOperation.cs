using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStoryPriorityOperation
    {
        private const string StoryPriorityChanged = "Story {0} priority is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public ChangeStoryPriorityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string ChangeStoryPriority(string teamToChangeStoryPriorityFor, string boardToChangeStoryPriorityFor, string storyToChangePriorityFor, string newStoryPriority, string authorOfStoryPriorityChange)
        {
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

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor, storyToChangePriorityFor);

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

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            return string.Format(StoryPriorityChanged, storyToChangePriorityFor, newPriorityEnum);

        }
    }
}
