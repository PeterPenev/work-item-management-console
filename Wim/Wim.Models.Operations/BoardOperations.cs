using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class BoardOperations : IBoardOperations
    {
        //Methods
        public string ShowBoardActivityToString(IBoard board)
        {
            int activityCounter = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"=======Board: {board.Name}'s Activity History========");
            foreach (var history in board.ActivityHistory)
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

        public void AddWorkitemToBoard(IBoard board, IWorkItem workItemToAdd)
        {
            board.WorkItems.Add(workItemToAdd);
        }

        public void AddActivityHistoryToBoard(IBoard board, IMember trackedMember, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} was created by Member: {trackedMember.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            board.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToBoard(IBoard board, IWorkItem trackedWorkItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            board.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryToBoard<T>(IBoard board, IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {trackedMember.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            board.ActivityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryAfterAssignUnsignToBoard(IBoard board, string itemType, string workItemTitle, IMember memberToAssign, IMember memberFromUnsign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {itemType} with Title: {workItemTitle} was unassigned from {memberFromUnsign.Name} and assigned to {memberToAssign.Name}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            board.ActivityHistory.Add(activityHistoryToAddToBoard);
        }
    }
}
