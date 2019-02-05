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
        private List<IActivityHistory> activityHistory;

        //constructor
        public Board(string name)
        {
            this.Name = name;
            this.workItems = new List<IWorkItem>();
            this.activityHistory = new List<IActivityHistory>();            
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

        public void AddWorkitemToBoard(IWorkItem workItemToAdd)
        {
            workItems.Add(workItemToAdd);
        }

        public void AddActivityHistoryToBoard(IBoard boardToAddHistoryTo, IMember trackedMember, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            activityHistory.Add(activityHistoryToAddToBoard);
        }
    }
}
