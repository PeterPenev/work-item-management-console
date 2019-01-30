using System;
using System.Collections.Generic;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IBug
    {
        Guid Id { get; set; }

        List<string> StepsToReproduce { get; set; }

        Priority Priority { get; }

        Severity Severity { get; }

        BugStatus BugStatus { get; }

        IMember Asignee { get; set; }
    }
}
