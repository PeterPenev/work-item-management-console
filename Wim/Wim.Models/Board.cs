using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    class Board : IBoard
    {
        //fiels
        private string name;
        private List<WorkItem> workItems;
        private List<ActivityHistory> activityHistory;

        //constructor
        public Board(string name)
        {
            this.name = name;
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

        public List<WorkItem> WorkItems
        {
            get
            {
                return new List<WorkItem>(this.workItems);
            }
        }

        public List<ActivityHistory> ActivityHistory
        {
            get
            {
                return new List<ActivityHistory>(this.activityHistory);
            }
        }

        //methods

    }
}
