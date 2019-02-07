using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Bug : WorkItem, IBug
    {
        private IList<string> stepsToReproduce;
        private Priority priority;
        private Severity severity;
        private BugStatus bugStatus = BugStatus.Active;
        private IMember assignee;

        //Constructor
        public Bug(string title, Priority priority, Severity severity, IMember assignee, IList<string> stepsToReproduce, string description)
            : base(title, description)
        {
            this.stepsToReproduce = new List<string>(stepsToReproduce);
            this.Priority = priority;
            this.Severity = severity;
            this.Assignee = assignee;

        }
        public IList<string> StepsToReproduce
        {
            get
            {
                return new List<string>(stepsToReproduce);
            }
        }

        public Priority Priority
        {
            get
            {
                return this.priority;
            }
            private set
            {
                if (value != Priority.Low && value != Priority.Medium && value != Priority.High)
                {
                    throw new ArgumentOutOfRangeException("Priority can be Low, Medium or High");
                }
                this.priority = value;
            }
        }

        public Severity Severity
        {
            get
            {
                return this.severity;
            }
            private set
            {
                if (value != Severity.Critical && value != Severity.Major && value != Severity.Minor)
                {
                    throw new ArgumentOutOfRangeException("Severity can be Minor, Major or Critical");
                }
                this.severity = value;
            }
        }


        public BugStatus BugStatus
        {
            get
            {
                return this.bugStatus;
            }
            private set
            {
                if (value != BugStatus.Active && value != BugStatus.Fixed)
                {
                    throw new ArgumentOutOfRangeException("Status can be Active or Fixed");
                }
                this.bugStatus = value;
            }
        }

        public IMember Assignee
        {
            get
            {
               return this.assignee;
            }
            set
            {
                this.assignee = value;
            }
        }

        public void ChangeBugPriority(Priority priority)
        {
            this.Priority = priority;
        }

        public void ChangeBugSeverity(Severity severity)
        {
            this.Severity = severity;
        }

        public void ChangeBugStatus(BugStatus status)
        {
            this.BugStatus = status;
        }
    }
}
