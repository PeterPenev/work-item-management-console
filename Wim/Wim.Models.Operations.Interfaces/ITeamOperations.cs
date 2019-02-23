using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface ITeamOperations
    {
        void AddMember(ITeam team, IMember addToTeam);

        void AddBoard(ITeam team, IBoard addToBoard);

        string ShowAllTeamBoards(ITeam team);

        string ShowAllTeamMembers(ITeam team);

        string ShowTeamActivityToString(ITeam team);
        
    }
}
