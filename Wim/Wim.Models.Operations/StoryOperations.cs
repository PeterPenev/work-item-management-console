using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class StoryOperations : WorkItemOperations, IStoryOperations
    {
        public void ChangeStoryPriority(IStory story, Priority priority)
        {
            story.Priority = priority;
        }

        public void ChangeStorySize(IStory story, Size size)
        {
            story.Size = size;
        }

        public void ChangeStoryStatus(IStory story, StoryStatus status)
        {

            story.StoryStatus = status;
        }

        public void AssignMemberToStory(IStory story, IMember memberToAssignBug)
        {
            story.Assignee = memberToAssignBug;
        }
    }
}
