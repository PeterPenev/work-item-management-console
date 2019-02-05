using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IAllTeams
    {
        IDictionary<string,ITeam> AllTeamsList { get; }

        void AddTeam(ITeam team);

        string ShowAllTeamsToString();

    }
}
