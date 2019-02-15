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
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";

      
        private const string TeamCreated = "Team with name {0} was created!";
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";

        private const string BugCreated = "Bug {0} was created!";
        private const string StoryCreated = "Story {0} was created!";
        private const string FeedbackCreated = "Feedback {0} was created!";


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

       

        public string CreateTeam(string teamName)
        {
            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValidateIfTeamExists(allTeams, teamName);

            //Operations
            var team = this.factory.CreateTeam(teamName);
            allTeams.AddTeam(team);

            return string.Format(TeamCreated, teamName);
        }

        public string CreateBoardToTeam(string boardToAddToTeam, string teamForAddingBoardTo)
        {
            //Validations
            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddToTeam, boardTypeForChecking);

            var teamTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(teamForAddingBoardTo, teamTypeForChecking);

            inputValidator.ValdateBoardNameLength(boardToAddToTeam);

            inputValidator.ValidateTeamExistance(allTeams, teamForAddingBoardTo);

            inputValidator.ValidateBoardAlreadyInTeam(allTeams, boardToAddToTeam, teamForAddingBoardTo);

            //Operations
            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }

        public string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAssignee, IList<string> bugStepsToReproduce, string bugDescription)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugTitle, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddBugFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddBugFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(bugTitle);

            inputValidator.ValdateItemDescriptionLength(bugDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddBugFor);

            inputValidator.ValidateMemberExistance(allMembers, bugAssignee);

            inputValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddBugFor, bugAssignee);

            inputValidator.ValidateBugExistanceInBoard(allTeams, boardToAddBugFor, teamToAddBugFor, bugTitle);

            //Operations
            Priority bugPriorityEnum = this.enumParser.GetPriority(bugPriority);
            Severity bugSeverityEnum = this.enumParser.GetSeverity(bugSeverity);
            IBug bugToAddToCollection = this.factory.CreateBug(bugTitle, bugPriorityEnum, bugSeverityEnum, allMembers.AllMembersList[bugAssignee], bugStepsToReproduce, bugDescription);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddBugFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddBugFor);

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(bugToAddToCollection);

            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddWorkItemIdToMember(bugToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor];

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, bugToAddToCollection);
            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddActivityHistoryToMember(bugToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(BugCreated, bugTitle);
        }

        public string CreateStory(string storyTitle, string teamToAddStoryFor, string boardToAddStoryFor, string storyPriority, string storySize, string storyStatus, string storyAssignee, string storyDescription)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyTitle, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddStoryFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddStoryFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(storyTitle);

            inputValidator.ValdateItemDescriptionLength(storyDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddStoryFor);

            inputValidator.ValidateMemberExistance(allMembers, storyAssignee);

            inputValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddStoryFor, storyAssignee);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddStoryFor, teamToAddStoryFor);

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToAddStoryFor, teamToAddStoryFor, storyTitle);

            //Operations
            Priority storyPriorityEnum = this.enumParser.GetPriority(storyPriority);
            Size storySizeEnum = this.enumParser.GetStorySize(storySize);
            StoryStatus storyStatusEnum = this.enumParser.GetStoryStatus(storyStatus);

            IStory storyToAddToCollection = this.factory.CreateStory(storyTitle, storyDescription, storyPriorityEnum, storySizeEnum, storyStatusEnum, allMembers.AllMembersList[storyAssignee]);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddStoryFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddStoryFor);

            allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(storyToAddToCollection);

            allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee).AddWorkItemIdToMember(storyToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor];

            allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, storyToAddToCollection);
            allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee).AddActivityHistoryToMember(storyToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(StoryCreated, storyTitle);
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
