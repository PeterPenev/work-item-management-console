using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeBugSeverityOperation : IEngineOperations
    {
        private const string BugSeverityChanged = "Bug {0} severity is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IBugOperations bugOperations;

        public ChangeBugSeverityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IBugOperations bugOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.bugOperations = bugOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeBugSeverityFor = inputParameters[0];
            string boardToChangeBugSeverityFor = inputParameters[1];
            string bugToChangeSeverityFor = inputParameters[2];
            string newSeverity = inputParameters[3];
            string authorOfBugSeverityChange = inputParameters[4];

            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeSeverityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugSeverityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugSeverityFor, boardTypeForChecking);

            var severityTypeForChecking = "Severity";
            inputValidator.IsNullOrEmpty(newSeverity, severityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugSeverityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugSeverityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor, bugToChangeSeverityFor);

            //Operations
            var itemType = "Bug";

            var newSeverityEnum = enumParser.GetSeverity(newSeverity);

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            bugOperations.ChangeBugSeverity(castedBugForPriorityChange, newSeverityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugSeverityFor, authorOfBugSeverityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugSeverityFor, itemType, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, newSeverity);

            bugOperations.AddActivityHistoryToWorkItem(bugToAddActivityFor, memberToAddActivityFor, newSeverity);

            return string.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
        }
    }
}
