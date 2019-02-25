using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllTeams : IAllTeams
    {
        //Constructors
        public AllTeams()
        {
            this.AllTeamsList = new Dictionary<string, ITeam>();
        }

        //Properties
        public IDictionary<string, ITeam> AllTeamsList { get; }       
    }
}
