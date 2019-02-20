using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamMembersOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowAllTeamMembersOperation(
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
            string teamToShowMembersFor = inputParameters[0];
            
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowMembersFor, teamTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToShowMembersFor);

            var allTeamMembersStringResult = allTeams.AllTeamsList[teamToShowMembersFor].ShowAllTeamMembers();
            return string.Format(allTeamMembersStringResult);
        }
    }
}
