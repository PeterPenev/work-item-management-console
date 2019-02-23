using System;
using System.Collections.Generic;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Story : WorkItem, IStory
    {
        //Constructors
        public Story(string title, string description, 
            Priority priority, 
            Size size, 
            StoryStatus storyStatus, 
            IMember assignee)
                : base(title, description)
        {
            this.Priority = priority;
            this.Size = size;
            this.StoryStatus = storyStatus;
            this.Assignee = assignee;
        }

        //Properties
        public Priority Priority { get; set; }

        public Size Size { get; set; }

        public StoryStatus StoryStatus { get; set; }

        public IMember Assignee { get; set; }
    }
}
