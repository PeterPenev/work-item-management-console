using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IAllTeams
    {
        List<ITeam> AllTeams { get; }
    }
}
