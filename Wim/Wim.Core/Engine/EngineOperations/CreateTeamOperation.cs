using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateTeamOperation : IEngineOperations
    {
        private const string TeamCreated = "Team with name {0} was created!";

        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;

        public CreateTeamOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IWimFactory factory)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.factory = factory;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamName = inputParameters[0];
            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            businessLogicValidator.ValidateIfTeamExists(allTeams, teamName);

            //Operations
            var team = this.factory.CreateTeam(teamName);
            allTeams.AddTeam(team);

            return string.Format(TeamCreated, teamName);
        }

    }
}
