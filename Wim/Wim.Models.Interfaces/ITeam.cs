using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface ITeam
    {
        string Name { get; set; }
        List<Board> Boards { get; set; }
        List<Member> Members { get; set; }
    }
}
