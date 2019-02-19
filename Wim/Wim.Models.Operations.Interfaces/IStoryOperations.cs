using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IStoryOperations
    {
        void ChangeStoryPriority(IStory story, Priority priority);

        void ChangeStorySize(IStory story, Size size);

        void ChangeStoryStatus(IStory story, StoryStatus status);

        void AssignMemberToStory(IStory story, IMember memberToAssignBug);
       
    }
}
