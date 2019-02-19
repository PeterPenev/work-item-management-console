using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IStoryOperations : IWorkItemOperations
    {
        void ChangeStoryPriority(IWorkItem workItem, Priority priority);

        void ChangeStorySize(IWorkItem workItem, Size size);

        void ChangeStoryStatus(IWorkItem workItem, StoryStatus status);

        void AssignMemberToStory(IWorkItem workItem, IMember memberToAssignBug);
       
    }
}
