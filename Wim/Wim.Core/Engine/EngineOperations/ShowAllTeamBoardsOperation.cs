using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamBoardsOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowAllTeamBoardsOperation(
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
            string teamToShowBoardsFor = inputParameters[0];
            
            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardsFor, teamTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToShowBoardsFor);

            businessLogicValidator.ValdateIfBoardsExistInTeam(allTeams, teamToShowBoardsFor);

            //Operations
            var allTeamBoardsResult = allTeams.AllTeamsList[teamToShowBoardsFor].ShowAllTeamBoards();
            return string.Format(allTeamBoardsResult);
        }
    }
}
