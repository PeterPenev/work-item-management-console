using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Member : IMember
    {
        private string name;

        public bool isAssigned = false;

        private List<IActivityHistory> activityHistory;

        private List<IWorkItem> workItems;

        public Member(string name)
        {
            this.Name = name;
        }

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

        //methods

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

        public string ShowMemberActivityToString(IList<IActivityHistory> activityHistoryInput)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfHistories = 1;

            foreach (var history in activityHistoryInput)
            {
                sb.AppendLine($"{numberOfHistories}. Activity with date: {history.LoggingDate}");
                sb.AppendLine($"Activity Message:");
                sb.AppendLine($"{history.Message}");
                numberOfHistories++;
            }

            return sb.ToString().Trim();
        }

    }
}
