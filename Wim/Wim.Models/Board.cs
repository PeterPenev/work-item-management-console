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

        //Methods
        public string ShowBoardActivityToString()
        {
            int activityCounter = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"=======Board: {this.Name}'s Activity History========");
            foreach (var history in this.activityHistory)
            {
                var formattedDate = String.Format("{0:r}", history.LoggingDate);
                sb.AppendLine($"{activityCounter}. Activity with date: {formattedDate}");
                sb.AppendLine($"Activity Message:");
                sb.AppendLine($"{history.Message}");
                activityCounter++;
            }
            sb.AppendLine("************************************************");
            return sb.ToString().Trim();
        }

        public void AddWorkitemToBoard(IWorkItem workItemToAdd)
        {
            this.workItems.Add(workItemToAdd);
        }

        public void AddActivityHistoryToBoard(IMember trackedMember, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToBoard(IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToBoard<T>(IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {trackedMember.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryAfterAssignUnsignToBoard(string itemType, string workItemTitle, IMember memberToAssign, IMember memberFromUnsign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {itemType} with Title: {workItemTitle} was unassigned from {memberFromUnsign.Name} and assigned to {memberToAssign.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }
    }
}
