using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public class WimFactory : IWimFactory
    {      
        public ITeam CreateTeam(string name)
        {
            return new Team(name);
        }

        public IMember CreateMember(string name)
        {
            return new Member(name);
        }

        IBoard IWimFactory.CreateBoard(string name)
        {
            return new Board(name);
        }       
    }
}
