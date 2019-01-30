using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    class Board : IBoard
    {
        //fiels
        private string name;
        private List<IWorkItem> workItems;
        private List<ActivityHistory> activityHistory;

        //constructor
        public Board(string name)
        {
            this.Name = name;
        }

        //properties
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public List<IWorkItem> WorkItems
        {
            get
            {
                return new List<IWorkItem>(this.workItems);
            }
        }

        public List<IActivityHistory> ActivityHistory
        {
            get
            {
                return new List<IActivityHistory>(this.activityHistory);
            }
        }

        //methods

    }
}
