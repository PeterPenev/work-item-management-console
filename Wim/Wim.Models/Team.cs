using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Team : ITeam
    {
        private List<IBoard> boards;

        private List<IMember> members;

        public Team(string name)
        {
            this.Name = name;
            boards = new List<IBoard>();
            members = new List<IMember>();
        }

        public string Name { get; private set; }

        public List<IBoard> Boards
        {
            get
            {
                return new List<IBoard>(this.boards);
            }
        }

        public List<IMember> Members
        {
            get
            {
                return new List<IMember>(this.members);
            }
        }

        public string ShowTeamsActivityToString(List<IMember> allMembersList)
        {
            StringBuilder sb = new StringBuilder();
            int numberOfActivities = 1;

           foreach(var member in allMembersList)
            {
                sb.AppendLine($"======={member.Key}'s Activity History=======");
                var memberActivityHistory = member.Value.ShowMemberActivityToString(member.Value.ActivityHistory);
                sb.AppendLine(memberActivityHistory);
                sb.AppendLine($"===========End Of {member.Key}'s Activity History================");
            }

            return sb.ToString().Trim();
        }
    }
}
