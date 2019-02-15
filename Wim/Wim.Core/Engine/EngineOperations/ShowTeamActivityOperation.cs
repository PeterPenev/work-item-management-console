using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowTeamActivityOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowTeamActivityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string ShowTeamActivity(string teamName)
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
    }
}
