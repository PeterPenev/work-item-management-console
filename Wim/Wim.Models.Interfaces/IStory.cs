using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IStory : IWorkItem
    {
        Priority Priority { get; set; }

        IMember Assignee { get; set; }

        Size Size { get; set; }

        StoryStatus StoryStatus { get; set; }
    }
}
