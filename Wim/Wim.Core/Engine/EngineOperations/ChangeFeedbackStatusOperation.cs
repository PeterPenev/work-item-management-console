using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeFeedbackStatusOperation : IEngineOperations
    {
        private const string FeedbackStatusChanged = "Feedback {0} status is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IFeedbackOperations feedbackOperations;

        public ChangeFeedbackStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IFeedbackOperations feedbackOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.feedbackOperations = feedbackOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeFeedbackStatusFor = inputParameters[0];
            string boardToChangeFeedbackStatusFor = inputParameters[1];
            string feedbackToChangeStatusFor = inputParameters[2];
            string newFeedbackStatus = inputParameters[3];
            string authorOfFeedbackStatusChange = inputParameters[4];
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

            feedbackOperations.AddActivityHistoryToWorkItem(feedbackToAddActivityFor, memberToAddActivityFor, newFeedbackStatus);

            return string.Format(FeedbackStatusChanged, feedbackToChangeStatusFor, newStatusEnum);
        }
    }
}
