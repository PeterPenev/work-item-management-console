using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeFeedbackStatusOperation
    {
        private const string FeedbackStatusChanged = "Feedback {0} status is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public ChangeFeedbackStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string ChangeFeedbackStatus(string teamToChangeFeedbackStatusFor, string boardToChangeFeedbackStatusFor, string feedbackToChangeStatusFor, string newFeedbackStatus, string authorOfFeedbackStatusChange)
        {
            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackToChangeStatusFor, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeFeedbackStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeFeedbackStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newFeedbackStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfFeedbackStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeFeedbackStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeFeedbackStatusFor, teamToChangeFeedbackStatusFor);

            inputValidator.ValidateNoSuchFeedbackInBoard(allTeams, boardToChangeFeedbackStatusFor, teamToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            //Operations
            var itemType = "Feedback";

            var newStatusEnum = enumParser.GetFeedbackStatus(newFeedbackStatus);

            var castedFeedbackForStatusChange = allTeams.FindFeedbackAndCast(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            castedFeedbackForStatusChange.ChangeFeedbackStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeFeedbackStatusFor, authorOfFeedbackStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeFeedbackStatusFor];

            var feedbackToAddActivityFor = allTeams.FindWorkItem(teamToChangeFeedbackStatusFor, itemType, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeFeedbackStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(feedbackToAddActivityFor, teamToFindIn, boardToAddActivityFor, newFeedbackStatus);

            feedbackToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackStatus);

            return string.Format(FeedbackStatusChanged, feedbackToChangeStatusFor, newStatusEnum);
        }
    }
}
