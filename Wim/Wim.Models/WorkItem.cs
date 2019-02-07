using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public abstract class WorkItem : IWorkItem
    {
        private string title;
        private string description;
        private IList<string> comments;
        private IList<IActivityHistory> activityHistory;

        public WorkItem(string title, string description)
        {
            this.Title = title;
            this.Description = description;
            this.Id = Guid.NewGuid();
            this.comments = new List<string>();
            this.activityHistory = new List<IActivityHistory>();
        }

        public Guid Id { get; private set; }

        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                if(value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Title should be between 10 and 50 symbols");
                }
                this.title = value;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
            private set
            {
                if(value.Length < 10 || value.Length > 500)
                {
                    throw new ArgumentOutOfRangeException("Description should be between 10 and 500 symbols");
                }
                this.description = value;
            }
           
        }
        public List<string> Comments
        {
            get
            {
                return new List<string>(this.comments);
            }
        }
        public List<IActivityHistory> ActivityHistory
        {
            get
            {
                return new List<IActivityHistory>(this.activityHistory);
            }
        }

        public void AddActivityHistoryToWorkItem(IMember trackedMember, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToWorkItem<T>(IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {trackedMember.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddComment(string commentToAdd, string authorOfComment)
        {
            this.Comments.Add($"{commentToAdd} with author{authorOfComment}");
        }
    }
}
