using System;
using System.Collections.Generic;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IBug : IWorkItem
    {
        IList<string> StepsToReproduce { get;}

        Priority Priority { get; }

        Severity Severity { get; }

        BugStatus BugStatus { get; }

        IMember Asignee { get; set; }
    }
}
