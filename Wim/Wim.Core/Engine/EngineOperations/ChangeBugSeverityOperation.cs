using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
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
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IMemberOpertaions memberOpertaions;


        public ChangeBugSeverityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IBugOperations bugOperations,
            IBusinessLogicValidator businessLogicValidator,
            IMemberOpertaions memberOpertaions)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.bugOperations = bugOperations;
            this.businessLogicValidator = businessLogicValidator;
            this.memberOpertaions = memberOpertaions;
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

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToChangeBugSeverityFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor);

            businessLogicValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor, bugToChangeSeverityFor);

            //Operations
            var itemType = "Bug";

            var isEnumConvertable = Enum.TryParse(newSeverity, out Severity newSeverityEnum);

            inputValidator.IsEnumConvertable(isEnumConvertable, "Severity");

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            bugOperations.ChangeBugSeverity(castedBugForPriorityChange, newSeverityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugSeverityFor, authorOfBugSeverityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugSeverityFor, itemType, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            memberOpertaions.AddActivityHistoryToMember(memberToAddActivityFor, bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, newSeverity);

            bugOperations.AddActivityHistoryToWorkItem(bugToAddActivityFor, memberToAddActivityFor, newSeverity);

            return string.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
        }
    }
}
