using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;


namespace Wim.Models
{
    public abstract class WorkItem : IWorkItem
    {
        //Constructors
        public WorkItem(string title, string description)
        {
            this.Title = title;
            this.Description = description;
            this.Id = Guid.NewGuid();
            this.Comments = new List<string>();
            this.ActivityHistory = new List<IActivityHistory>();
        }

        //Properties
        public Guid Id { get; private set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IList<string> Comments { get; }

        public IList<IActivityHistory> ActivityHistory { get; }       
    }
}
