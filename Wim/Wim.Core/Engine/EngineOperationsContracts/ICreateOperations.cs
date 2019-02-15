using System.Collections.Generic;

namespace Wim.Core.Engine.EngineOperationsContracts
{
    public interface ICreateOperations
    {
        string CreatePerson(string personName);

        string CreateTeam(string teamName);

        string CreateBoardToTeam(string boardToAddToTeam, string teamForAddingBoardTo);

        string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAssignee, IList<string> bugStepsToReproduce, string bugDescription);

        string CreateStory(string storyTitle, string teamToAddStoryFor, string boardToAddStoryFor, string storyPriority, string storySize, string storyStatus, string storyAssignee, string storyDescription);

        string CreateFeedback(string feedbackTitle, string teamToAddFeedbackFor, string boardToAddFeedbackFor, string feedbackRaiting, string feedbackStatus, string feedbackDescription);

        string AddComment(string teamToAddCommentToWorkItemFor, string boardToAddCommentToWorkItemFor, string itemTypeToAddWorkItemFor, string workitemToAddCommentFor, string authorOfComment, string commentToAdd);

        string AssignUnassignItem(string teamToAssignUnsignItem, string boardToAssignUnsignItem, string itemType, string itemToAssignUnsign, string memberToAssignItem);

        string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo);
    }
}

