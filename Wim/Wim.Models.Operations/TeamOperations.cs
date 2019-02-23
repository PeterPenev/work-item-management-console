using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class TeamOperations : ITeamOperations
    {
        private readonly MemberOperations memberOperations;

        public TeamOperations(MemberOperations memberOperations)
        {
            this.memberOperations = memberOperations;
        }

        public void AddMember(ITeam team, IMember addToTeam)
        {
            team.Members.Add(addToTeam);
        }

        public void AddBoard(ITeam team, IBoard addToBoard)
        {
            team.Boards.Add(addToBoard);
        }

        public string ShowAllTeamBoards(ITeam team)
        {
            int teamBoards = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team name: {team.Name}");

            foreach (var selectedBoard in team.Boards)
            {
                sb.AppendLine($"{teamBoards}. {selectedBoard.Name}");
                teamBoards++;
            }

            return sb.ToString().Trim();
        }

        public string ShowAllTeamMembers(ITeam team)
        {
            int teamMembers = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team name: {team.Name}");

            foreach (var selectedMember in team.Members)
            {
                sb.AppendLine($"{teamMembers}. {selectedMember.Name}");
                teamMembers++;
            }

            return sb.ToString().Trim();
        }

        public string ShowTeamActivityToString(ITeam team)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"==============TEAM: {team.Name}'s Activity History==============");
            foreach (var member in team.Members)
            {
                var memberActivityHistory = this.memberOperations.ShowMemberActivityToString(member);
                sb.AppendLine(memberActivityHistory);
            }
            sb.AppendLine($"****************End Of TEAM{team.Name}'s Activity History*****************");
            return sb.ToString().Trim();
        }
    }
}
