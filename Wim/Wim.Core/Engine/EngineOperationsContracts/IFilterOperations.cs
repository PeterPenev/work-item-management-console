namespace Wim.Core.Engine.EngineOperationsContracts
{
    public interface IFilterOperations
    {
        string FilterBugs();

        string FilterStories();

        string FilterFeedbacks();

        string FilterBugsByPriority(string priorityToFilterBugFor);

        string FilterBugsByAssignee(string assigneeToFilterBugFor);

        string FilterBugsByStatus(string statusToFilterBugFor);

        string FilterStoriesByPriority(string priorityToFilterStoryFor);

        string FilterStoriesByAssignee(string assigneeToFilterStoryFor);

        string FilterStoriesByStatus(string statusToFilterStoryFor);

        string FilterFeedbacksByStatus(string statusToFilterFeedbacksFor);
    }
}
