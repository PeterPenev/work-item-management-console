﻿using System.Collections.Generic;
using System.Text;
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

        //methods
        //Returning String Representation of the Teams'names in the Dictionary of allTeamsInput
        public string ShowAllTeamsToString(IDictionary<string, IMember> allTeamsInput)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfTeam = 1;

            foreach (var team in allTeamsInput)
            {
                sb.AppendLine($"{numberOfTeam}. {team.Key}");
                numberOfTeam++;
            }

            return sb.ToString().Trim();
        }

    }
}
