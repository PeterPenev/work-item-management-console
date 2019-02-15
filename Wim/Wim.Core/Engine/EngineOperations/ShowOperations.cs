using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.Engine.EngineOperationsContracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowOperations : IShowOperations
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public ShowOperations(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
        }

        public string ShowAllPeople()
        {
            //Validations
            inputValidator.ValdateIfAnyMembersExist(allMembers);

            //Operations
            var peopleToDisplay = allMembers.ShowAllMembersToString();

            return string.Format(peopleToDisplay);
        }

        public string ShowAllTeams()
        {
            //Validations
            inputValidator.ValdateIfAnyTeamsExist(allTeams);

            //Operations
            var teamsToDisplay = allTeams.ShowAllTeamsToString();

            return string.Format(teamsToDisplay);
        }

        public string ShowMemberActivityToString(string memberName)
        {
            //Validations
            var inputTypeForChecking = "Member Name";
            inputValidator.IsNullOrEmpty(memberName, inputTypeForChecking);

            inputValidator.ValidateMemberExistance(allMembers, memberName);

            //Operations
            var selectedMember = this.allMembers.AllMembersList[memberName];
            var memberActivities = selectedMember.ShowMemberActivityToString();

            return string.Format(memberActivities);
        }

        public string ShowTeamActivityToString(string teamName)
        {
            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValdateIfAnyTeamsExist(allTeams);

            inputValidator.ValidateTeamExistance(allTeams, teamName);

            //Operations
            var teamToCheckHistoryFor = allTeams.AllTeamsList[teamName];
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString();

            return string.Format(teamActivityHistory);
        }

        public string ShowAllTeamMembers(string teamToShowMembersFor)
        {
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowMembersFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowMembersFor);

            var allTeamMembersStringResult = allTeams.AllTeamsList[teamToShowMembersFor].ShowAllTeamMembers();
            return string.Format(allTeamMembersStringResult);
        }

        public string ShowAllTeamBoards(string teamToShowBoardsFor)
        {
            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardsFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardsFor);

            inputValidator.ValdateIfBoardsExistInTeam(allTeams, teamToShowBoardsFor);

            //Operations
            var allTeamBoardsResult = allTeams.AllTeamsList[teamToShowBoardsFor].ShowAllTeamBoards();
            return string.Format(allTeamBoardsResult);
        }


        public string ShowBoardActivityToString(string teamToShowBoardActivityFor, string boardActivityToShow)
        {
            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardActivityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardActivityToShow, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardActivityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardActivityToShow, teamToShowBoardActivityFor);

            //Operations
            var boardToDisplayActivityFor = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow).FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }

        public string ListAllWorkItems()
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            //Operations
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL WORK ITEMS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

    }
}
