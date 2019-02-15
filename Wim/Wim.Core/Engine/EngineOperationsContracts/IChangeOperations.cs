namespace Wim.Core.Engine.EngineOperationsContracts
{
    public interface IChangeOperations
    {
        string ChangeBugPriority(string teamToChangeBugPriorityFor, string boardToChangeBugPriorityFor, string bugToChangePriorityFor, string priority, string authorOfBugPriorityChange);

        string ChangeBugSeverity(string teamToChangeBugSeverityFor, string boardToChangeBugSeverityFor, string bugToChangeSeverityFor, string newSeverity, string authorOfBugSeverityChange);

        string ChangeBugStatus(string teamToChangeBugStatusFor, string boardToChangeBugStatusFor, string bugToChangeStatusFor, string newStatus, string authorOfBugStatusChange);

        string ChangeStoryPriority(string teamToChangeStoryPriorityFor, string boardToChangeStoryPriorityFor, string storyToChangePriorityFor, string newStoryPriority, string authorOfStoryPriorityChange);

        string ChangeStorySize(string teamToChangeStorySizeFor, string boardToChangeStorySizeFor, string storyToChangeSizeFor, string newStorySize, string authorOfStorySizeChange);

        string ChangeStoryStatus(string teamToChangeStoryStatusFor, string boardToChangeStoryStatusFor, string storyToChangeStatusFor, string newStoryStatus, string authorOfStoryStatusChange);

        string ChangeFeedbackRating(string teamToChangeFeedbackRatingFor, string boardToChangeFeedbackRatingFor, string feedbackToChangeRatingFor, string newFeedbackRating, string authorOfFeedbackRatingChange);

        string ChangeFeedbackStatus(string teamToChangeFeedbackStatusFor, string boardToChangeFeedbackStatusFor, string feedbackToChangeStatusFor, string newFeedbackStatus, string authorOfFeedbackStatusChange);

        string AssignUnassignItem(string teamToAssignUnsignItem, string boardToAssignUnsignItem, string itemType, string itemToAssignUnsign, string memberToAssignItem);
    }
}
