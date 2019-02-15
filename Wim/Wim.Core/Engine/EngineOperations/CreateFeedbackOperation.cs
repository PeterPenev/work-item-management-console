using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateFeedbackOperation
    {
        private const string FeedbackCreated = "Feedback {0} was created!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;

        public CreateFeedbackOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser,
            IWimFactory factory)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
            this.factory = factory;
        }

        public string CreateFeedback(string feedbackTitle, string teamToAddFeedbackFor, string boardToAddFeedbackFor, string feedbackRaiting, string feedbackStatus, string feedbackDescription)
        {
            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackTitle, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddFeedbackFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddFeedbackFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(feedbackTitle);

            inputValidator.ValdateItemDescriptionLength(feedbackDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddFeedbackFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor);

            inputValidator.ValidateFeedbackExistanceInBoard(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor, feedbackTitle);

            var intFeedbackRating = inputValidator.ValidateRatingConversion(feedbackRaiting);

            //Operations
            FeedbackStatus feedbackStatusEnum = this.enumParser.GetFeedbackStatus(feedbackStatus);

            IFeedback feedbackToAddToCollection = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, intFeedbackRating, feedbackStatusEnum);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddFeedbackFor);

            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(feedbackToAddToCollection);
            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(feedbackToAddToCollection);

            return string.Format(FeedbackCreated, feedbackTitle);
        }
    }
}
