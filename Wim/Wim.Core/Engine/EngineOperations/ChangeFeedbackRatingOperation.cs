using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeFeedbackRatingOperation : IEngineOperations
    {
        private const string FeedbackRatingChanged = "Feedback {0} rating is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public ChangeFeedbackRatingOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeFeedbackRatingFor = inputParameters[0];
            string boardToChangeFeedbackRatingFor = inputParameters[1];
            string feedbackToChangeRatingFor = inputParameters[2];
            string newFeedbackRating = inputParameters[3];
            string authorOfFeedbackRatingChange = inputParameters[4];

            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackToChangeRatingFor, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeFeedbackRatingFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeFeedbackRatingFor, boardTypeForChecking);

            var ratingTypeForChecking = "Rating";
            inputValidator.IsNullOrEmpty(newFeedbackRating, ratingTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfFeedbackRatingChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeFeedbackRatingFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeFeedbackRatingFor, teamToChangeFeedbackRatingFor);

            inputValidator.ValidateNoSuchFeedbackInBoard(allTeams, boardToChangeFeedbackRatingFor, teamToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            var integerRating = inputValidator.ValidateRatingConversion(newFeedbackRating);

            //Operations  
            var itemType = "Feedback";

            var castedFeedbackForRatingChange = allTeams.FindFeedbackAndCast(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            castedFeedbackForRatingChange.ChangeFeedbackRating(integerRating);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeFeedbackRatingFor, authorOfFeedbackRatingChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeFeedbackRatingFor];

            var feedbackToAddActivityFor = allTeams.FindWorkItem(teamToChangeFeedbackRatingFor, itemType, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeFeedbackRatingFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackRating);

            memberToAddActivityFor.AddActivityHistoryToMember(feedbackToAddActivityFor, teamToFindIn, boardToAddActivityFor, newFeedbackRating);

            feedbackToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackRating);

            return string.Format(FeedbackRatingChanged, feedbackToChangeRatingFor, integerRating);
        }

    }
}
