using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Story : WorkItem, IStory
    {
        //fields
        private Priority priority;
        private Size size;
        private StoryStatus storyStatus;
        private Member assignee;

        //constructor
        public Story(string title, string description, Priority priority, Size size, StoryStatus storyStatus, Member assignee)
            : base(title, description)
        {
            this.Priority = priority;
            this.Size = size;
            this.StoryStatus = storyStatus;
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
        public StoryStatus StoryStatus
        {
            get
            {
                return this.storyStatus;
            }
            set
            {
                this.storyStatus = value;
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
