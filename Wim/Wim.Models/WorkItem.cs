using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public abstract class WorkItem : IWorkItem
    {
        //Fields
        private string title;
        private string description;
        private IList<string> comments;
        private IList<IActivityHistory> activityHistory;

        //Constructors
        public WorkItem(string title, string description)
        {
            this.Title = title;
            this.Description = description;
            this.Id = Guid.NewGuid();
            this.comments = new List<string>();
            this.activityHistory = new List<IActivityHistory>();
        }

        //Properties
        public Guid Id { get; private set; }

        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {               
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

        //Methods
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
