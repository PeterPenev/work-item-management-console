using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateFeedbackOperation : IEngineOperations
    {
        private const string FeedbackCreated = "Feedback {0} was created!";

        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IWimFactory factory;
        private readonly IDescriptionBuilder descriptionBuilder;
        private readonly IBoardOperations boardOperations;

        public CreateFeedbackOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IWimFactory factory,
            IDescriptionBuilder descriptionBuilder,
            IBoardOperations boardOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.factory = factory;
            this.descriptionBuilder = descriptionBuilder;
            this.boardOperations = boardOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Assign Values
            string feedbackTitle = inputParameters[0];
            string teamToAddFeedbackFor = inputParameters[1];
            string boardToAddFeedbackFor = inputParameters[2];
            string feedbackRaiting = inputParameters[3];
            string feedbackStatus = inputParameters[4];
            var feedbackDescription = descriptionBuilder.BuildDescription(inputParameters, 5);

            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackTitle, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddFeedbackFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddFeedbackFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(feedbackTitle);

            inputValidator.ValdateItemDescriptionLength(feedbackDescription);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToAddFeedbackFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor);

            businessLogicValidator.ValidateFeedbackExistanceInBoard(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor, feedbackTitle);

            var intFeedbackRating = inputValidator.ValidateRatingConversion(feedbackRaiting);

            //Operations
            var isEnumConvertable = Enum.TryParse(feedbackStatus, out FeedbackStatus feedbackStatusEnum);

            inputValidator.IsEnumConvertable(isEnumConvertable, "FeedbackStatus");

            IFeedback feedbackToAddToCollection = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, intFeedbackRating, feedbackStatusEnum);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddFeedbackFor);

            boardOperations.AddWorkitemToBoard(allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam], feedbackToAddToCollection);
            boardOperations.AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam], feedbackToAddToCollection);

            return string.Format(FeedbackCreated, feedbackTitle);
        }
    }
}
