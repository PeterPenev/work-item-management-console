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
        private IMemberOpertaions memberOpertaions;

        //Constructors
        public Team(string name, IMemberOpertaions memberOpertaions)
        {
            this.Name = name;
            boards = new List<IBoard>();
            members = new List<IMember>();
            this.memberOpertaions = memberOpertaions;
        }

        //Properties
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

        public void AddMember(IMember addToTeam)
        {
            members.Add(addToTeam);
        }

        public void AddBoard(IBoard addToBoard)
        {
            boards.Add(addToBoard);
        }

        //Methods
        public string ShowAllTeamBoards()
        {
            int teamBoards = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team name: {this.Name}");

            foreach (var selectedBoard in this.boards)
            {
                sb.AppendLine($"{teamBoards}. {selectedBoard.Name}");
                teamBoards++;
            }

            return sb.ToString().Trim();
        }

        public string ShowAllTeamMembers()
        {
            int teamMembers = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team name: {this.Name}");

            foreach(var selectedMember in this.members)
            {
                sb.AppendLine($"{teamMembers}. {selectedMember.Name}");
                teamMembers++;
            }
           
            return sb.ToString().Trim();
        }

        public string ShowTeamActivityToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"==============TEAM: {this.Name}'s Activity History==============");
            foreach (var member in this.Members)
            {                
                var memberActivityHistory = this.memberOpertaions.ShowMemberActivityToString(member);
                sb.AppendLine(memberActivityHistory);                
            }
            sb.AppendLine($"****************End Of TEAM{this.Name}'s Activity History*****************");
            return sb.ToString().Trim();
        }
    }
}
