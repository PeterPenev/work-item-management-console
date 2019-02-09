using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IStory : IWorkItem
    {
        Priority Priority { get; }

        IMember Assignee { get; }

        Size Size { get; }

        StoryStatus StoryStatus { get; }

        void ChangeStoryPriority(Priority priority);

        void ChangeStorySize(Size size);

        void ChangeStoryStatus(StoryStatus status);

        void AssignMemberToStory(IMember memberToAssignStory);
    }
}
