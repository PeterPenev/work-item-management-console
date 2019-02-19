using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class BugOperations : WorkItemOperations, IBugOperations
    {
        public void ChangeBugPriority(IWorkItem workItem, Priority priority)
        {
            var bug = workItem as Bug;
            bug.Priority = priority;
        }

        public void ChangeBugSeverity(IWorkItem workItem, Severity severity)
        {
            var bug = workItem as Bug;
            bug.Severity = severity;
        }

        public void ChangeBugStatus(IWorkItem workItem, BugStatus status)
        {
            var bug = workItem as Bug;
            bug.BugStatus = status;
        }

        public void AssignMemberToBug(IWorkItem workItem, IMember memberToAssignBug)
        {
            var bug = workItem as Bug;
            bug.Assignee = memberToAssignBug;
        }
    }
}
