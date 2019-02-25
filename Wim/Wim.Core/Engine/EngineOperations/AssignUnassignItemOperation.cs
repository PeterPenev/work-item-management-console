using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class AssignUnassignItemOperation : IEngineOperations
    {
        private const string AssignItemTo = "{0} with name: {1} on board {2} part of team {3} was assigned to member {4}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IStoryOperations storyOperations;
        private readonly IBugOperations bugOperations;
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IMemberOpertaions memberOpertaions;
        private readonly IBoardOperations boardOperations;


        public AssignUnassignItemOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IStoryOperations storyOperations,
            IBugOperations bugOperations,
            IBusinessLogicValidator businessLogicValidator,
            IMemberOpertaions memberOpertaions,
            IBoardOperations boardOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.bugOperations = bugOperations;
            this.storyOperations = storyOperations;
            this.businessLogicValidator = businessLogicValidator;
            this.memberOpertaions = memberOpertaions;
            this.boardOperations = boardOperations;
        }

        public string Execute(IList<string> inputParametes)
        {
            //Assign Values
            string teamToAssignUnsignItem = inputParametes[0];
            string boardToAssignUnsignItem = inputParametes[1];
            string itemType = inputParametes[2];
            string itemToAssignUnsign = inputParametes[3];
            string memberToAssignItem = inputParametes[4];

            //Validations
            var itemTypeForChecking = "Item Title";
            inputValidator.IsNullOrEmpty(itemToAssignUnsign, itemTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAssignUnsignItem, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAssignUnsignItem, boardTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(memberToAssignItem, authorTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToAssignUnsignItem);

            businessLogicValidator.ValidateMemberExistance(allMembers, memberToAssignItem);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToAssignUnsignItem, teamToAssignUnsignItem);

            //Operations
            var itemMemberToAssign = allTeams.FindMemberInTeam(teamToAssignUnsignItem, memberToAssignItem);

            var itemToChangeIn = allTeams.FindWorkItem(teamToAssignUnsignItem, itemType, boardToAssignUnsignItem, itemToAssignUnsign);

            IMember itemMemberBeforeUnssign = null;

            if (itemType == "Bug")
            {
                var typedItem = (Bug)itemToChangeIn;

                itemMemberBeforeUnssign = typedItem.Assignee;

                bugOperations.AssignMemberToBug(typedItem, itemMemberToAssign);

                memberOpertaions.RemoveWorkItemIdToMember(itemMemberToAssign, typedItem.Id);

                memberOpertaions.AddWorkItemIdToMember(itemMemberToAssign, typedItem.Id);
            }
            else if (itemType == "Story")
            {
                var typedItem = (Story)itemToChangeIn;

                itemMemberBeforeUnssign = typedItem.Assignee;

                storyOperations.AssignMemberToStory(typedItem, itemMemberToAssign);

                memberOpertaions.RemoveWorkItemIdToMember(itemMemberToAssign, typedItem.Id);

                memberOpertaions.AddWorkItemIdToMember(itemMemberToAssign, typedItem.Id);
            }

            //history
            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAssignUnsignItem].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAssignUnsignItem);

            //add history to board
            boardOperations.AddActivityHistoryAfterAssignUnsignToBoard(allTeams.AllTeamsList[teamToAssignUnsignItem].Boards[indexOfBoardInSelectedTeam], itemType, itemToAssignUnsign, itemMemberToAssign, itemMemberBeforeUnssign);

            //add history to member before unssign

            memberOpertaions.AddActivityHistoryAfterUnsignToMember(itemMemberBeforeUnssign, itemType, itemToAssignUnsign);

            //add history to member after assign
            memberOpertaions.AddActivityHistoryAfterAssignToMember(itemMemberToAssign, itemType, itemToAssignUnsign);

            return string.Format(AssignItemTo, itemType, itemToAssignUnsign, boardToAssignUnsignItem, teamToAssignUnsignItem, memberToAssignItem);
        }
    }
}
