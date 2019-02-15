namespace Wim.Core.Engine.EngineOperationsContracts
{
    public interface ISortOperations
    {
        string SortBugsBy(string factorToSortBy);

        string SortStoriesBy(string factorToSortBy);

        string SortFeedbacksBy(string factorToSortBy);        
    }
}
