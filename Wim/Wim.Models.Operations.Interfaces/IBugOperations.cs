using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IBugOperations
    {
        void ChangeBugPriority(IBug bug, Priority priority);

        void ChangeBugSeverity(IBug bug, Severity severity);

        void ChangeBugStatus(IBug bug, BugStatus status);

        void AssignMemberToBug(IBug bug, IMember memberToAssignBug);
    }
}
