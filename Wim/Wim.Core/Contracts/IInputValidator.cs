using Wim.Models.Interfaces;

namespace Wim.Core.Contracts
{
    public interface IInputValidator
    {
        void IsNullOrEmpty(string inputToCheck, string inputType);

        void ValdateIfAnyTeamsExist(IAllTeams allTeams);

        void ValdateIfAnyMembersExist(IAllMembers allMembers);

        void ValdateIfBoardsExistInTeam(IAllTeams allTeams, string teamToShowBoardsFor);

        void ValidateMemberExistance(IAllMembers allMembers, string memberName);

        void ValidateTeamExistance(IAllTeams allTeams, string teamName);

        void ValidateBoardExistance(IAllTeams allTeams, string boardToAddToTeam, string teamForAddingBoardTo);

        void ValidateBugExistanceInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle);

        void ValidateBugNotInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle);

        void ValidateStoryExistanceInBoard(IAllTeams allTeams, string boardToAddStoryFor, string teamToAddStoryFor, string storyTitle);

        void ValidateFeedbackExistanceInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle);

        void ValidateIfPersonExists(IAllMembers allMembers, string personName);

        void ValidateIfTeamExists(IAllTeams allTeams, string teamName);

        void ValidateIfAnyWorkItemsExist(IAllTeams allTeams);

        void ValidateIfAnyBugsExist(IAllTeams allTeams);

        void ValidateIfAnyStoriesExist(IAllTeams allTeams);

        void ValidateIfAnyFeedbacksExist(IAllTeams allTeams);

        void ValidateBoardExistanceInTeam(IAllTeams allTeams, string boardNameToCheckFor, string teamToCheckForBoard);

        int ValidateRatingConversion(string ratingForCheck);

    }
}
