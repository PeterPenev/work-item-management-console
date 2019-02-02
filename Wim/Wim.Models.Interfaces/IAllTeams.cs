using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IAllTeams
    {
        Dictionary<string,ITeam> AllTeamsList { get; }
    }
}
