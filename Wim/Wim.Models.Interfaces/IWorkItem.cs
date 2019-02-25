using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IWorkItem
    {
        Guid Id { get;  }

        string Title { get; }

        string Description { get; }

        IList<string> Comments { get; }

        IList<IActivityHistory> ActivityHistory { get; }
    }
}
