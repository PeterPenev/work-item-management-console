using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class AddCommentOperation : IEngineOperations
    {
        private const string AddedCommentFor = "Comment {0} with author {1} is added to {2} with name: {3}.";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IWorkItemOperations workItemOperations;

        public AddCommentOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IWorkItemOperations workItemOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.workItemOperations = workItemOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Assign Values From List Of Parameters
            string teamToAddCommentToWorkItemFor = inputParameters[0];
            string boardToAddCommentToWorkItemFor = inputParameters[1];
            string itemTypeToAddWorkItemFor = inputParameters[2];
            string workitemToAddCommentFor = inputParameters[3];
            string authorOfComment = inputParameters[4];
            string commentToAdd = inputParameters[5];

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

            workItemOperations.AddComment(workItemToAddCommentTo, commentToAdd, authorOfComment);

            return string.Format(AddedCommentFor, commentToAdd, authorOfComment, itemTypeToAddWorkItemFor, workitemToAddCommentFor);
        }
    }
}
