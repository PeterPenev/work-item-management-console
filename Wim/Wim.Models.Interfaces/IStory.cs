using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IStory : IWorkItem
    {
        Priority Priority { get; }

        Size Size { get; }

        Status Status { get; }

        Member Asignee { get; }
    }
}
