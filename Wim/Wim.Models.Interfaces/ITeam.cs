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

        void Add(IMember addToTeam);

        string ShowTeamActivityToString(List<IMember> allTeamMembersList);
    }
}
