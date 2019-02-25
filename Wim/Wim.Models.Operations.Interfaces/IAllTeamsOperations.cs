using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IAllTeamsOperations
    {
        void AddTeam(IAllTeams allTeams, ITeam team);

        string ShowAllTeamsToString(IAllTeams allTeams);
    }
}
