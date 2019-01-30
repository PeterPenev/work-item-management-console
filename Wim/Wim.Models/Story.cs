using System;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Story : WorkItem, IStory
    {
        //fields
        private Priority priority;
        private Size size;
        private Status status;
        private Member assignee;

        //constructor
        public Story(string title, string description, Priority priority, Size size, Status status, Member assignee)
            : base(title, description)
        {
            this.Priority = priority;
            this.Size = size;
            this.Status = status;
            this.Assignee = assignee;
        }

        //properties
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
        public Size Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }
        public Status Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public Member Assignee
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
                     
        //methods


    }
}
