using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class AddCommentOperation
    {
        private const string AddedCommentFor = "Comment {0} with author {1} is added to {2} with name: {3}.";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public AddCommentOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string AddComment(string teamToAddCommentToWorkItemFor, string boardToAddCommentToWorkItemFor, string itemTypeToAddWorkItemFor, string workitemToAddCommentFor, string authorOfComment, string commentToAdd)
        {
            //Validations
            var itemTypeForChecking = "Item Title";
            inputValidator.IsNullOrEmpty(workitemToAddCommentFor, itemTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddCommentToWorkItemFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddCommentToWorkItemFor, boardTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfComment, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddCommentToWorkItemFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddCommentToWorkItemFor, teamToAddCommentToWorkItemFor);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateItemExistanceInBoard(allTeams, boardToAddCommentToWorkItemFor, teamToAddCommentToWorkItemFor, workitemToAddCommentFor);

            //Operations
            var workItemToAddCommentTo = allTeams.FindWorkItem(teamToAddCommentToWorkItemFor, itemTypeToAddWorkItemFor, boardToAddCommentToWorkItemFor, workitemToAddCommentFor);

            workItemToAddCommentTo.AddComment(commentToAdd, authorOfComment);

            return string.Format(AddedCommentFor, commentToAdd, authorOfComment, itemTypeToAddWorkItemFor, workitemToAddCommentFor);
        }
    }
}
