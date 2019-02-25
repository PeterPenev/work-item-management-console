using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowTeamsActivityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly ITeamOperations teamOperations;

        public ShowTeamsActivityOperation(
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
            string teamName = inputParameters[0];

            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            businessLogicValidator.ValdateIfAnyTeamsExist(allTeams);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamName);

            //Operations
            var teamToCheckHistoryFor = allTeams.AllTeamsList[teamName];

            var teamActivityHistory = teamOperations.ShowTeamActivityToString(teamToCheckHistoryFor);
            
            return string.Format(teamActivityHistory);
        }
    }
}
