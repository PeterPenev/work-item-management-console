using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Bug : WorkItem, IBug
    {
        //Constructors
        public Bug(string title, 
            Priority priority, 
            Severity severity, 
            IMember assignee, 
            IList<string> stepsToReproduce,
            string description)
            : base(title, description)
        {
            this.StepsToReproduce = new List<string>(stepsToReproduce);
            this.Priority = priority;
            this.Severity = severity;
            this.Assignee = assignee;

        }

        //Properties
        public IList<string> StepsToReproduce { get; }

        public string Test { get; set; }

        public Priority Priority { get; set; }

        public Severity Severity { get; set; }

        public BugStatus BugStatus { get; set; }

        public IMember Assignee { get; set; }      
    }
}
