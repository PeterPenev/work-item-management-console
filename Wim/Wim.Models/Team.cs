using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    class Team : ITeam
    {
        private List<Board> boards;

        private List<Member> members;

        public Team(string name)
        {
            this.Name = name;
            boards = new List<Board>();
            members = new List<Member>();
        }

        public string Name { get; private set; }

        List<Board> Boards
        {
            get
            {
                return new List<Board>(this.board);
            }
        }
        List<Member> Members
        {
            get
            {
                return new List<Member>(this.member);
            }
        }
    }
}
