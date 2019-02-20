using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowBoardActivityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowBoardActivityOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string Execute(IList<string> inputParameters) 
        {
            //Assign Values From List Of Parameters
            string teamToShowBoardActivityFor = inputParameters[0];
            string boardActivityToShow = inputParameters[1];

            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardActivityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardActivityToShow, boardTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToShowBoardActivityFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardActivityToShow, teamToShowBoardActivityFor);

            //Operations
            var boardToDisplayActivityFor = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow)
                .FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }
    }
}
