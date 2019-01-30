using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IStory : IWorkItem
    {
        Priority Priority { get; }

        Size Size { get; }

        StoryStatus StoryStatus { get; }
    }
}
