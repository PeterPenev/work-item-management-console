using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllTeams
    {
        //field
        private List<ITeam> allTeamsList;

        //constructor
        public AllTeams()
        {
            this.allTeamsList = new List<ITeam>();
        }

        //properties
        public List<ITeam> AllTeamsList
        {
            get
            {
                return new List<ITeam>(this.allTeamsList);
            }
        }


    }
}
