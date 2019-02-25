using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamBoardsOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly ITeamOperations teamOperations;

        public ShowAllTeamBoardsOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            ITeamOperations teamOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.teamOperations = teamOperations;
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
            var teamToShowBoard = allTeams.AllTeamsList[teamToShowBoardsFor];
            var allTeamBoardsResult = teamOperations.ShowAllTeamBoards(teamToShowBoard);

            return string.Format(allTeamBoardsResult);
        }
    }
}
