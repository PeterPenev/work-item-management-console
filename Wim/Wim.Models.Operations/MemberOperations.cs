using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class MemberOperations : IMemberOpertaions
    {
        public void AddWorkItemIdToMember(IMember member, Guid workItemIdInput)
        {
            member.WorkItemsId.Add(workItemIdInput);
        }        

        public void AddActivityHistoryToMember(IMember member, IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Member: {member.Name} created: {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} in Board: {trackedBoard.Name} part of {trackedTeam.Name} Team!");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            member.ActivityHistory.Add(activityHistoryToAddToMember);
        }

        public void AddActivityHistoryToMember<T>(IMember member,IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {member.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            member.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryAfterAssignToMember(IMember member, string itemType, string workItemTitle, IMember memberToAssign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{itemType} with Title: {workItemTitle} was assigned to member {memberToAssign.Name}!");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            member.ActivityHistory.Add(activityHistoryToAddToMember);
        }

        public void AddActivityHistoryAfterUnsignToMember(IMember member, string itemType, string workItemTitle, IMember memberFromUnsign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{itemType} with Title: {workItemTitle} was unssigned from member {memberFromUnsign.Name}!"); ;
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            member.ActivityHistory.Add(activityHistoryToAddToMember);
        }

        public void RemoveWorkItemIdToMember(IMember member, Guid workItemIdInput)
        {
            member.WorkItemsId.Remove(workItemIdInput);
        }

        public string ShowMemberActivityToString(IMember member)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfHistories = 1;
            sb.AppendLine($"=======Member: {member.Name}'s Activity History========");
            foreach (var history in member.ActivityHistory)
            {
                var formattedDate = String.Format("{0:r}", history.LoggingDate);
                sb.AppendLine($"{numberOfHistories}. Activity with date: {formattedDate}");
                sb.AppendLine($"Activity Message:");
                sb.AppendLine($"{history.Message}");
                numberOfHistories++;
            }
            sb.AppendLine("************************************************");

            return sb.ToString().Trim();
        }
    }
}
