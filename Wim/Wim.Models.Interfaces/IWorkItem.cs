using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IWorkItem
    {
        string Title { get; }

        string Description { get; }

        List<string> Comments { get; }

        List<string> History { get; }
    }
}
