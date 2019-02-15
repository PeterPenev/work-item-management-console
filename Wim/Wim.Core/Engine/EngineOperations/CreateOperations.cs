using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.Engine.EngineOperationsContracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateOperations : ICreateOperations
    {
        private const string AddedCommentFor = "Comment {0} with author {1} is added to {2} with name: {3}.";
        private const string AssignItemTo = "{0} with name: {1} on board {2} part of team {3} was assigned to member {4}!";  

      
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;

        public CreateOperations(
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

        public string AssignUnassignItem(string teamToAssignUnsignItem, string boardToAssignUnsignItem, string itemType, string itemToAssignUnsign, string memberToAssignItem)
        {
            //Validations
            var itemTypeForChecking = "Item Title";
            inputValidator.IsNullOrEmpty(itemToAssignUnsign, itemTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAssignUnsignItem, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAssignUnsignItem, boardTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(memberToAssignItem, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAssignUnsignItem);

            inputValidator.ValidateMemberExistance(allMembers, memberToAssignItem);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAssignUnsignItem, teamToAssignUnsignItem);

            //Operations
            var itemMemberToAssign = allTeams.FindMemberInTeam(teamToAssignUnsignItem, memberToAssignItem);

            var itemToChangeIn = allTeams.FindWorkItem(teamToAssignUnsignItem, itemType, boardToAssignUnsignItem, itemToAssignUnsign);

            IMember itemMemberBeforeUnssign = null;

            if (itemType == "Bug")
            {
                var typedItem = (Bug)itemToChangeIn;

                itemMemberBeforeUnssign = typedItem.Assignee;

                typedItem.AssignMemberToBug(itemMemberToAssign);

                itemMemberBeforeUnssign.RemoveWorkItemIdToMember(typedItem.Id);

                itemMemberToAssign.AddWorkItemIdToMember(typedItem.Id);
            }
            else if (itemType == "Story")
            {
                var typedItem = (Story)itemToChangeIn;

                itemMemberBeforeUnssign = typedItem.Assignee;

                typedItem.AssignMemberToStory(itemMemberToAssign);

                itemMemberBeforeUnssign.RemoveWorkItemIdToMember(typedItem.Id);

                itemMemberToAssign.AddWorkItemIdToMember(typedItem.Id);
            }

            //history
            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAssignUnsignItem].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAssignUnsignItem);

            //add history to board
            allTeams.AllTeamsList[teamToAssignUnsignItem].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryAfterAssignUnsignToBoard(itemType, itemToAssignUnsign, itemMemberToAssign, itemMemberBeforeUnssign);

            //add history to member before unssign
            itemMemberBeforeUnssign.AddActivityHistoryAfterUnsignToMember(itemType, itemToAssignUnsign, itemMemberBeforeUnssign);

            //add history to member after assign
            itemMemberToAssign.AddActivityHistoryAfterAssignToMember(itemType, itemToAssignUnsign, itemMemberToAssign);

            return string.Format(AssignItemTo, itemType, itemToAssignUnsign, boardToAssignUnsignItem, teamToAssignUnsignItem, memberToAssignItem);
        }

        public string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo)
        {
            //Validations
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personToAddToTeam, personTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddPersonTo, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddPersonTo);

            inputValidator.ValidateMemberExistance(allMembers, personToAddToTeam);

            inputValidator.ValidateIfMemberAlreadyInTeam(allTeams, teamToAddPersonTo, personToAddToTeam);

            //Operations
            allTeams.AllTeamsList[teamToAddPersonTo].AddMember(allMembers.AllMembersList[personToAddToTeam]);
            return string.Format(PersonAddedToTeam, personToAddToTeam, teamToAddPersonTo);
        }
    }
}
