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

        List<string> Comments { get; }

        List<string> History { get; }
    }
}
