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
        private bool isAssigned = false;
        private IAllTeams allTeams;

        //Constructors
        public Member(string name, IAllTeams allTeams)
        {
            this.Name = name;
            this.allTeams = allTeams;
            this.ActivityHistory = new List<IActivityHistory>();
            this.WorkItemsId = new List<Guid>();
        }

        //Properties
        public string Name { get; set; }       

        public List<Guid> WorkItemsId { get; }

        public List<IActivityHistory> ActivityHistory { get; }

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
