using System;
using System.Collections.Generic;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IBug : IWorkItem
    {
        List<string> StepsToReproduce { get; set; }

        Priority Priority { get; }

        Severity Severity { get; }

        BugStatus BugStatus { get; }

        IMember Asignee { get; set; }
    }
}
