using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;
using Wim.Models;

namespace Wim.Models.Operations
{
    public class WorkItemOperations
    {
        public void AddActivityHistoryToWorkItem(IWorkItem itemToAddActivityHistoryFor, IMember trackedMember, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            itemToAddActivityHistoryFor.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToWorkItem<T>(IWorkItem itemToAddActivityHistoryFor, IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {trackedMember.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            itemToAddActivityHistoryFor.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddComment(IWorkItem itemToAddActivityHistoryFor, string commentToAdd, string authorOfComment)
        {
            itemToAddActivityHistoryFor.Comments.Add($"{commentToAdd} with author{authorOfComment}");
        }
    }
}
