using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllTeams : IAllTeams
    {
        //field
        private Dictionary<string, ITeam> allTeamsList;

        //constructor
        public AllTeams()
        {
            this.allTeamsList = new Dictionary<string, ITeam>();
        }

        //properties
        public IDictionary<string, ITeam> AllTeamsList
        {
            get
            {
                return new Dictionary<string, ITeam>(this.allTeamsList);
            }
        }


    }
}
