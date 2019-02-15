using System;
using System.Collections.Generic;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IBug : IWorkItem
    {
        IList<string> StepsToReproduce { get; }

        Priority Priority { get; set; }

        Severity Severity { get; }

        BugStatus BugStatus { get; }

        IMember Assignee { get; }

        void ChangeBugPriority(Priority priority);

        void ChangeBugSeverity(Severity severity);

        void ChangeBugStatus(BugStatus status);

        void AssignMemberToBug(IMember memberToAssignBug);
    }
}
