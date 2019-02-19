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
        public void ChangeStoryPriority(IWorkItem workItem, Priority priority)
        {
            var story = workItem as Story;
            story.Priority = priority;
        }

        public void ChangeStorySize(IWorkItem workItem, Size size)
        {
            var story = workItem as Story;
            story.Size = size;
        }

        public void ChangeStoryStatus(IWorkItem workItem, StoryStatus status)
        {
            var story = workItem as Story;
            story.StoryStatus = status;
        }

        public void AssignMemberToStory(IWorkItem workItem, IMember memberToAssignBug)
        {
            var story = workItem as Story;
            story.Assignee = memberToAssignBug;
        }
    }
}
