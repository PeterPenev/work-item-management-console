using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Board : IBoard
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
        public string ShowBoardActivityToString(IList<IActivityHistory> activityHistoryInput)
        {
            int activityCounter = 1;
            StringBuilder sb = new StringBuilder();

            foreach (var history in activityHistoryInput)
            {
                sb.AppendLine($"{activityCounter}. Activity with date: {history.LoggingDate}");
                sb.AppendLine($"Activity Message:");
                sb.AppendLine($"{history.Message}");
                activityCounter++;
            }

            return sb.ToString().Trim();
        }

    }
}
