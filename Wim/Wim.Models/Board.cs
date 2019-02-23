using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Board : IBoard
    {
        //Fields
        private string name;
        private List<IWorkItem> workItems;
        private List<IActivityHistory> activityHistory;

        //Constructors
        public Board(string name)
        {
            this.Name = name;
            this.workItems = new List<IWorkItem>();
            this.activityHistory = new List<IActivityHistory>();
        }

        //Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {               
                this.name = value;
            }
        }

        public List<IWorkItem> WorkItems
        {
            get
            {
                return this.workItems;
            }
        }

        public List<IActivityHistory> ActivityHistory
        {
            get
            {
                return this.activityHistory;
            }
        }        
    }
}
