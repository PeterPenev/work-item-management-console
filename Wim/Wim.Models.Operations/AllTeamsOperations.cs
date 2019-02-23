using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class AllTeamsOperations : IAllTeamsOperations
    {
        //Methods

        public void AddTeam(IAllTeams allTeams, ITeam team)
        {
            allTeams.AllTeamsList.Add(team.Name, team);
        }

        public string ShowAllTeamsToString(IAllTeams allTeams)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfTeam = 1;

            foreach (var team in allTeams.AllTeamsList)
            {
                sb.AppendLine($"{numberOfTeam}. {team.Key}");
                numberOfTeam++;
            }

            return sb.ToString().Trim();
        }
    }
}
