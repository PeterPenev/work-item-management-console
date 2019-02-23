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
        private IAllTeams allTeams;

        //Constructors
        public Member(string name, IAllTeams allTeams)
        {
            this.Name = name;
            this.allTeams = allTeams;
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
                return this.workItemsId;
            }
        }

        public List<IActivityHistory> ActivityHistory
        {
            get
            {
                return this.activityHistory;
            }
        }

        public bool IsAssigned
        {
            get
            {
                foreach (var team in this.allTeams.AllTeamsList)
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
        }
    }
}
