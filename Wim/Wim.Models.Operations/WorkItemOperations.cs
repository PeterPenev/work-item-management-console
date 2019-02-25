using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;
using Wim.Models;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class WorkItemOperations : IWorkItemOperations
    {
        public void AddActivityHistoryToWorkItem(IWorkItem trackedWorkItem, IMember trackedMember)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            trackedWorkItem.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToWorkItem<T>(IWorkItem trackedWorkItem, IMember trackedMember, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {trackedMember.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            trackedWorkItem.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddComment(IWorkItem itemToAddActivityHistoryFor, string commentToAdd, string authorOfComment)
        {
            itemToAddActivityHistoryFor.Comments.Add($"{commentToAdd} with author{authorOfComment}");
        }
    }
}
