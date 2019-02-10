using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Member : IMember
    {
        //Fields
        private string name;
        private bool isAssigned = false;
        private List<IActivityHistory> activityHistory;
        private List<Guid> workItemsId;

        //Constructors
        public Member(string name)
        {
            this.Name = name;
            this.activityHistory = new List<IActivityHistory>();
            this.workItemsId = new List<Guid>();
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

        public List<Guid> WorkItemsId
        {
            get
            {
                return new List<Guid>(this.workItemsId);
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
        public void AddWorkItemIdToMember(Guid workItemIdInput)
        {
            this.workItemsId.Add(workItemIdInput);
        }

        public bool FindIfMemberIsAssigned(IDictionary<string, ITeam> allTeamsInput)
        {
            foreach (var team in allTeamsInput)
            {
                if (team.Value.Members.Contains(this))
                {
                    this.isAssigned = true;
                }
                else
                {
                    this.isAssigned = false;
                }
            }
            return isAssigned;
        }

        public void AddActivityHistoryToMember(IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Member: {this.Name} created: {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} in Board: {trackedBoard.Name} part of {trackedTeam.Name} Team!");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            activityHistory.Add(activityHistoryToAddToMember);
        }

        public void AddActivityHistoryToMember<T>(IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard, T changedEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"A {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title}'s {changedEnum.GetType().Name} was changed by Member: {this.Name} to {changedEnum}");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToBoard = new ActivityHistory(resultToAddAssMessage);
            this.activityHistory.Add(activityHistoryToAddToBoard);
        }

        public void AddActivityHistoryAfterAssignToMember(string itemType, string workItemTitle, IMember memberToAssign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{itemType} with Title: {workItemTitle} was assigned to member {memberToAssign.Name}!");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            activityHistory.Add(activityHistoryToAddToMember);
        }

        public void AddActivityHistoryAfterUnsignToMember(string itemType, string workItemTitle, IMember memberFromUnsign)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{itemType} with Title: {workItemTitle} was unssigned from member {memberFromUnsign.Name}!"); ;
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            activityHistory.Add(activityHistoryToAddToMember);
        }

        public void RemoveWorkItemIdToMember(Guid workItemIdInput)
        {
            this.workItemsId.Remove(workItemIdInput);
        }

        public string ShowMemberActivityToString()
        {
            StringBuilder sb = new StringBuilder();

            int numberOfHistories = 1;
            sb.AppendLine($"=======Member: {this.Name}'s Activity History========");            
            foreach (var history in this.ActivityHistory)
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
