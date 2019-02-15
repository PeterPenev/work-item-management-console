using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowBoardActivityOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowBoardActivityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string ShowBoardActivity(string teamToShowBoardActivityFor, string boardActivityToShow)
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
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow)
                .FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }
    }
}
