using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IBugOperations : IWorkItemOperations
    {
        void ChangeBugPriority(IWorkItem workItem, Priority priority);

        void ChangeBugSeverity(IWorkItem workItem, Severity severity);

        void ChangeBugStatus(IWorkItem workItem, BugStatus status);

        void AssignMemberToBug(IWorkItem workItem, IMember memberToAssignBug);
    }
}
