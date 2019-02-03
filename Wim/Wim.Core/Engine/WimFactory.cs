using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
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

        public IBoard CreateBoard(string name)
        {
            return new Board(name);
        }

        public IBoard CreateBug(string name)
        {
            return new Bug(name);
        }

        public IBoard CreateStory(string title, Priority priority, Size size, StoryStatus storyStatus, IMember asignee, string description)
        {
            return new Story(title, priority, size, storyStatus, asignee, description);
        }
    }
}
