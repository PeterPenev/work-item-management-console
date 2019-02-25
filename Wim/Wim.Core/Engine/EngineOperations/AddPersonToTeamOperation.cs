using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class AddPersonToTeamOperation : IEngineOperations
    {
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly ITeamOperations teamOperations;

        public AddPersonToTeamOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IBusinessLogicValidator businessLogicValidator,
            ITeamOperations teamOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.businessLogicValidator = businessLogicValidator;
            this.teamOperations = teamOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Assign Values From List Of Parameters
            string personToAddToTeam = inputParameters[0];
            string teamToAddPersonTo = inputParameters[1];

            //Validations
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personToAddToTeam, personTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddPersonTo, teamTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToAddPersonTo);

            businessLogicValidator.ValidateMemberExistance(allMembers, personToAddToTeam);

            businessLogicValidator.ValidateIfMemberAlreadyInTeam(allTeams, teamToAddPersonTo, personToAddToTeam);

            //Operations
            var teamToAddMember = allTeams.AllTeamsList[teamToAddPersonTo];

            this.teamOperations.AddMember(teamToAddMember, allMembers.AllMembersList[personToAddToTeam]);
            return string.Format(PersonAddedToTeam, personToAddToTeam, teamToAddPersonTo);
        }
    }
}
