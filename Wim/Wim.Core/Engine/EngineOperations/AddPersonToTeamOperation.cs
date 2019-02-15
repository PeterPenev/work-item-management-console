using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class AddPersonToTeamOperation
    {
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;

        public AddPersonToTeamOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;;
        }

        public string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo)
        {
            //Validations
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personToAddToTeam, personTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddPersonTo, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddPersonTo);

            inputValidator.ValidateMemberExistance(allMembers, personToAddToTeam);

            inputValidator.ValidateIfMemberAlreadyInTeam(allTeams, teamToAddPersonTo, personToAddToTeam);

            //Operations
            allTeams.AllTeamsList[teamToAddPersonTo].AddMember(allMembers.AllMembersList[personToAddToTeam]);
            return string.Format(PersonAddedToTeam, personToAddToTeam, teamToAddPersonTo);
        }
    }
}
