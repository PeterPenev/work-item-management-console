using System;
using System.Collections.Generic;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IBug : IWorkItem
    {
        IList<string> StepsToReproduce { get; }

        Priority Priority { get; set; }

        Severity Severity { get; set; }

        BugStatus BugStatus { get; set; }

        IMember Assignee { get; set; }
    }
}
