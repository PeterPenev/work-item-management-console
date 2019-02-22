using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Bug : WorkItem, IBug
    {
        //Fields
        private IList<string> stepsToReproduce;
        private Priority priority;
        private Severity severity;
        private BugStatus bugStatus = BugStatus.Active;
        private IMember assignee;

        //Constructors
        public Bug(string title, Priority priority, Severity severity, IMember assignee, IList<string> stepsToReproduce, string description)
            : base(title, description)
        {
            this.stepsToReproduce = new List<string>(stepsToReproduce);
            this.Priority = priority;
            this.Severity = severity;
            this.Assignee = assignee;

        }

        //Properties
        public IList<string> StepsToReproduce
        {
            get
            {
                return new List<string>(stepsToReproduce);
            }
        }

        public string Test { get; set; }

        public Priority Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public Severity Severity
        {
            get
            {
                return this.severity;
            }
            set
            {                
                this.severity = value;
            }
        }

        public BugStatus BugStatus
        {
            get
            {
                return this.bugStatus;
            }
            set
            {                
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
    }
}
