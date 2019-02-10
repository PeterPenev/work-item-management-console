using System;
using System.Collections.Generic;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Story : WorkItem, IStory
    {
        //Fields
        private Priority priority;
        private Size size;
        private StoryStatus storyStatus;
        private IMember assignee;

        //Constructors
        public Story(string title, string description, Priority priority, Size size, StoryStatus storyStatus, IMember assignee)
            : base(title, description)
        {
            this.Priority = priority;
            this.Size = size;
            this.StoryStatus = storyStatus;
            this.Assignee = assignee;
        }

        //Properties
        public Priority Priority
        {
            get
            {
                return this.priority;
            }
            private set
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
            private set
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
            private set
            {
                this.storyStatus = value;
            }
        }

        public IMember Assignee
        {
            get
            {
                return this.assignee;
            }
            private set
            {
                this.assignee = value;
            }
        }

        //Methods
        public void ChangeStoryPriority(Priority priority)
        {
            this.Priority = priority;
        }

        public void ChangeStorySize(Size size)
        {
            this.Size = size;
        }

        public void ChangeStoryStatus(StoryStatus status)
        {
            this.StoryStatus = status;
        }

        public void AssignMemberToStory(IMember memberToAssignStory)
        {
            this.Assignee = memberToAssignStory;
        }
    }
}
