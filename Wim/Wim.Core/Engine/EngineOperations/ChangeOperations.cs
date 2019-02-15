using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeOperations
    {
        public string ChangeBugPriority(string teamToChangeBugPriorityFor, string boardToChangeBugPriorityFor, string bugToChangePriorityFor, string priority, string authorOfBugPriorityChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangePriorityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugPriorityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor, bugToChangePriorityFor);

            //Operations
            var newPriorityEnum = enumParser.GetPriority(priority);

            var itemType = "Bug";

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            castedBugForPriorityChange.ChangeBugPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugPriorityFor, authorOfBugPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugPriorityFor, itemType, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, priority);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, priority);

            bugToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, bugToAddActivityFor, priority);

            return string.Format(BugPriorityChanged, bugToChangePriorityFor, newPriorityEnum);

        }

        private string ChangeBugSeverity(string teamToChangeBugSeverityFor, string boardToChangeBugSeverityFor, string bugToChangeSeverityFor, string newSeverity, string authorOfBugSeverityChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeSeverityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugSeverityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugSeverityFor, boardTypeForChecking);

            var severityTypeForChecking = "Severity";
            inputValidator.IsNullOrEmpty(newSeverity, severityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugSeverityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugSeverityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor, bugToChangeSeverityFor);

            //Operations
            var itemType = "Bug";

            var newSeverityEnum = enumParser.GetSeverity(newSeverity);

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            castedBugForPriorityChange.ChangeBugSeverity(newSeverityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugSeverityFor, authorOfBugSeverityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugSeverityFor, itemType, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, newSeverity);

            bugToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            return string.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
        }

        public string ChangeBugStatus(string teamToChangeBugStatusFor, string boardToChangeBugStatusFor, string bugToChangeStatusFor, string newStatus, string authorOfBugStatusChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeStatusFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor, bugToChangeStatusFor);

            //Operations
            var itemType = "Bug";

            var newStatusEnum = enumParser.GetBugStatus(newStatus);

            var castedBugToChangeStatusIn = allTeams.FindBugAndCast(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var bugToChangeStatus = allTeams.FindWorkItem(teamToChangeBugStatusFor, itemType, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var boardToChangeStatusIn = allTeams.FindBoardInTeam(teamToChangeBugStatusFor, boardToChangeBugStatusFor);

            var teamToChangeStatusOfBoardIn = allTeams.AllTeamsList[teamToChangeBugStatusFor];

            var memberToChangeActivityHistoryFor = allTeams.FindMemberInTeam(teamToChangeBugStatusFor, authorOfBugStatusChange);

            castedBugToChangeStatusIn.ChangeBugStatus(newStatusEnum);

            memberToChangeActivityHistoryFor
                .AddActivityHistoryToMember(bugToChangeStatus, teamToChangeStatusOfBoardIn, boardToChangeStatusIn, newStatusEnum);

            boardToChangeStatusIn
                .AddActivityHistoryToBoard(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            bugToChangeStatus
                .AddActivityHistoryToWorkItem(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            return string.Format(BugStatusChanged, bugToChangeStatusFor, newStatus);
        }

        public string ChangeStoryPriority(string teamToChangeStoryPriorityFor, string boardToChangeStoryPriorityFor, string storyToChangePriorityFor, string newStoryPriority, string authorOfStoryPriorityChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangePriorityFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(newStoryPriority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryPriorityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor, storyToChangePriorityFor);

            //Operations
            var itemType = "Story";

            var newPriorityEnum = enumParser.GetPriority(newStoryPriority);

            var castedStoryForPriorityChange = allTeams.FindStoryAndCast(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            castedStoryForPriorityChange.ChangeStoryPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryPriorityFor, authorOfStoryPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryPriorityFor, itemType, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryPriority);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            return string.Format(StoryPriorityChanged, storyToChangePriorityFor, newPriorityEnum);

        }

        public string ChangeStorySize(string teamToChangeStorySizeFor, string boardToChangeStorySizeFor, string storyToChangeSizeFor, string newStorySize, string authorOfStorySizeChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeSizeFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStorySizeFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStorySizeFor, boardTypeForChecking);

            var sizeTypeForChecking = "Size";
            inputValidator.IsNullOrEmpty(newStorySize, sizeTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStorySizeChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStorySizeFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor, storyToChangeSizeFor);

            //Operations
            var itemType = "Story";

            var newSizeEnum = enumParser.GetStorySize(newStorySize);

            var castedStoryForSizeChange = allTeams.FindStoryAndCast(teamToChangeStorySizeFor, boardToChangeStorySizeFor, storyToChangeSizeFor);

            castedStoryForSizeChange.ChangeStorySize(newSizeEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStorySizeFor, authorOfStorySizeChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStorySizeFor, itemType, boardToChangeStorySizeFor, storyToChangeSizeFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStorySizeFor, boardToChangeStorySizeFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStorySize);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            return string.Format(StoryPriorityChanged, storyToChangeSizeFor, newSizeEnum);
        }

        public string ChangeStoryStatus(string teamToChangeStoryStatusFor, string boardToChangeStoryStatusFor, string storyToChangeStatusFor, string newStoryStatus, string authorOfStoryStatusChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeStatusFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStoryStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor, storyToChangeStatusFor);


            //Operations
            var itemType = "Story";

            var newStatusEnum = enumParser.GetStoryStatus(newStoryStatus);

            var castedStoryForStatusChange = allTeams.FindStoryAndCast(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            castedStoryForStatusChange.ChangeStoryStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryStatusFor, authorOfStoryStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryStatusFor, itemType, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryStatus);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            return string.Format(StoryPriorityChanged, storyToChangeStatusFor, newStatusEnum);

        }

        public string ChangeFeedbackRating(string teamToChangeFeedbackRatingFor, string boardToChangeFeedbackRatingFor, string feedbackToChangeRatingFor, string newFeedbackRating, string authorOfFeedbackRatingChange)
        {
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
    }
}
