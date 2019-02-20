using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowTeamActivityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowTeamActivityOperation(IBusinessLogicValidator businessLogicValidator,
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
            string teamName = inputParameters[0];

            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            businessLogicValidator.ValdateIfAnyTeamsExist(allTeams);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamName);

            //Operations
            var teamToCheckHistoryFor = allTeams.AllTeamsList[teamName];
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString();

            return string.Format(teamActivityHistory);
        }
    }
}
