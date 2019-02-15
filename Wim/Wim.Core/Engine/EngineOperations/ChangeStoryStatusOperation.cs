using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStoryStatusOperation
    {
        private const string StoryStatusChanged = "Story {0} status is changed to{1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public ChangeStoryStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string ChangeStoryStatus(string teamToChangeStoryStatusFor, string boardToChangeStoryStatusFor, string storyToChangeStatusFor, string newStoryStatus, string authorOfStoryStatusChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeStatusFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStoryStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor, storyToChangeStatusFor);


            //Operations
            var itemType = "Story";

            var newStatusEnum = enumParser.GetStoryStatus(newStoryStatus);

            var castedStoryForStatusChange = allTeams.FindStoryAndCast(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            castedStoryForStatusChange.ChangeStoryStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryStatusFor, authorOfStoryStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryStatusFor, itemType, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryStatus);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            return string.Format(StoryStatusChanged, storyToChangeStatusFor, newStatusEnum);

        }
    }
}
