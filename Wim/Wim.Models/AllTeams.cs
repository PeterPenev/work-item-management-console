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
                return new Dictionary<string, ITeam>(this.allTeamsList);
            }
        }

        //Methods

        public void AddTeam(ITeam team)
        {
            allTeamsList.Add(team.Name, team);
        }

        public string ShowAllTeamsToString()
        {
            StringBuilder sb = new StringBuilder();

            int numberOfTeam = 1;

            foreach (var team in this.AllTeamsList)
            {
                sb.AppendLine($"{numberOfTeam}. {team.Key}");
                numberOfTeam++;
            }

            return sb.ToString().Trim();
        }
    }
}
