using Wim.Models.Interfaces;

namespace Wim.Core.Contracts
{
    public interface IInputValidator
    {
        void IsNullOrEmpty(string inputToCheck, string inputType);

        void ValdateIfAnyTeamsExist(IAllTeams allTeams);

        void ValdateIfAnyMembersExist(IAllMembers allMembers);

        void ValdateIfBoardsExistInTeam(IAllTeams allTeams, string teamToShowBoardsFor);

        void ValidateBoardExistanceInTeam(IAllTeams allTeams, string boardNameToCheckFor, string teamToCheckForBoard);

        void ValidateBoardAlreadyInTeam(IAllTeams allTeams, string boardToAddToTeam, string teamForAddingBoardTo);

        void ValidateItemExistanceInBoard(IAllTeams allTeams, string boardNameToCheckFor, string teamToCheckForBoard, string itemNameToCheckFor);

        void ValidateMemberExistance(IAllMembers allMembers, string memberName);

        void ValidateTeamExistance(IAllTeams allTeams, string teamName);

        void ValidateBugExistanceInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle);

        void ValidateBugNotInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle);

        void ValidateStoryExistanceInBoard(IAllTeams allTeams, string boardToAddStoryFor, string teamToAddStoryFor, string storyTitle);

        void ValidateFeedbackExistanceInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle);

        void ValidateNoSuchFeedbackInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle);

        void ValidateIfPersonExists(IAllMembers allMembers, string personName);

        void ValidateIfMemberAlreadyInTeam(IAllTeams allTeams, string teamToCheckForPerson, string personName);

        void ValidateIfMemberNotInTeam(IAllTeams allTeams, string teamToCheckForPerson, string personName);

        void ValidateIfTeamExists(IAllTeams allTeams, string teamName);

        void ValidateIfAnyWorkItemsExist(IAllTeams allTeams);

        void ValidateIfAnyBugsExist(IAllTeams allTeams);

        void ValidateIfAnyStoriesExist(IAllTeams allTeams);

        void ValidateIfAnyFeedbacksExist(IAllTeams allTeams);

        int ValidateRatingConversion(string ratingForCheck);
    }
}
