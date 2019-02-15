using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations
{
    public class BugOperations
    {
        //Methods
        public void ChangeBugPriority(Bug bug, Priority priority)
        {
            bug.Priority = priority;
        }

        public void ChangeBugSeverity(IBug bug, Severity severity)
        {
            this.Severity = severity;
        }

        public void ChangeBugStatus(BugStatus status)
        {
            this.BugStatus = status;
        }

        public void AssignMemberToBug(IMember memberToAssignBug)
        {
            this.Assignee = memberToAssignBug;
        }
    }
}
