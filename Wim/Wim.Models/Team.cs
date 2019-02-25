using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models
{
    public class Team : ITeam
    {
        //Constructors
        public Team(string name)
        {
            this.Name = name;
            this.Boards = new List<IBoard>();
            this.Members = new List<IMember>();
        }

        //Properties
        public string Name { get; }

        public List<IBoard> Boards { get; }

        public List<IMember> Members { get; }
    }
}
