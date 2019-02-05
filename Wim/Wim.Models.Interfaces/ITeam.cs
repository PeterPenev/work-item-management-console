using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface ITeam
    {
        string Name { get; }

        List<IBoard> Boards { get; }

        List<IMember> Members { get; }

        void AddMember(IMember addToTeam);

        void AddBoard(IBoard addToBoard);

        string ShowAllTeamMembers();

        string ShowAllTeamBoards();

        string ShowTeamActivityToString();


    }
}
