using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateTeamOperation : IEngineOperations
    {
        private const string TeamCreated = "Team with name {0} was created!";

        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IWimFactory factory;
        private readonly IAllTeamsOperations allTeamsOperations;
        private readonly IMemberOpertaions memberOpertaions;

        public CreateTeamOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IWimFactory factory,
            IAllTeamsOperations allTeamsOperations,
            IMemberOpertaions memberOpertaions)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.factory = factory;
            this.allTeamsOperations = allTeamsOperations;
            this.memberOpertaions = memberOpertaions;
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

            allTeamsOperations.AddTeam(allTeams, team);

            return string.Format(TeamCreated, teamName);
        }

    }
}
