using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamMembersOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowAllTeamMembersOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string ShowAllTeamMembers(string teamToShowMembersFor)
        {
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowMembersFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowMembersFor);

            var allTeamMembersStringResult = allTeams.AllTeamsList[teamToShowMembersFor].ShowAllTeamMembers();
            return string.Format(allTeamMembersStringResult);
        }
    }
}
