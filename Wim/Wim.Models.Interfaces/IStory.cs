using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IStory : IWorkItem
    {


        Guid Id { get; }


        Priority Priority { get; }

        Size Size { get; }

        Status Status { get; }

    }
}
