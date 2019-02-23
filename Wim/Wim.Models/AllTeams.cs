using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllTeams : IAllTeams
    {
        //Fields
        private Dictionary<string, ITeam> allTeamsList;

        //Constructors
        public AllTeams()
        {
            this.allTeamsList = new Dictionary<string, ITeam>();
        }

        //Properties
        public IDictionary<string, ITeam> AllTeamsList
        {
            get
            {
                return this.allTeamsList;
            }
        }

       
    }
}
