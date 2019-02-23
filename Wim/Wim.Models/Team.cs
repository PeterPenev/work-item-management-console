using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models
{
    public class Team : ITeam
    {
        //Fields
        private List<IBoard> boards;
        private List<IMember> members;

        //Constructors
        public Team(string name)
        {
            this.Name = name;
            boards = new List<IBoard>();
            members = new List<IMember>();
        }

        //Properties
        public string Name { get; private set; }

        public List<IBoard> Boards
        {
            get
            {
                return this.boards;
            }
        }

        public List<IMember> Members
        {
            get
            {
                return this.members;
            }
        }
    }
}
