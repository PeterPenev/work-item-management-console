using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations
{
    public class BugOperations
    {
        public void ChangeBugPriority(IBug bug, Priority priority)
        {
            bug.Priority = priority;
        }

        public void ChangeBugSeverity(IBug bug, Severity severity)
        {
            bug.Severity = severity;
        }

        public void ChangeBugStatus(IBug bug, BugStatus status)
        {
            bug.BugStatus = status;
        }

        public void AssignMemberToBug(IBug bug, IMember memberToAssignBug)
        {
            bug.Assignee = memberToAssignBug;
        }
    }
}
