using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamBoardsOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowAllTeamBoardsOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
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
    }
}
