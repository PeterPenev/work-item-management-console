using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeBugStatusOperation
    {
        private const string BugStatusChanged = "Bug {0} status is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public ChangeBugStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string ChangeBugStatus(string teamToChangeBugStatusFor, string boardToChangeBugStatusFor, string bugToChangeStatusFor, string newStatus, string authorOfBugStatusChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeStatusFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor, bugToChangeStatusFor);

            //Operations
            var itemType = "Bug";

            var newStatusEnum = enumParser.GetBugStatus(newStatus);

            var castedBugToChangeStatusIn = allTeams.FindBugAndCast(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var bugToChangeStatus = allTeams.FindWorkItem(teamToChangeBugStatusFor, itemType, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var boardToChangeStatusIn = allTeams.FindBoardInTeam(teamToChangeBugStatusFor, boardToChangeBugStatusFor);

            var teamToChangeStatusOfBoardIn = allTeams.AllTeamsList[teamToChangeBugStatusFor];

            var memberToChangeActivityHistoryFor = allTeams.FindMemberInTeam(teamToChangeBugStatusFor, authorOfBugStatusChange);

            castedBugToChangeStatusIn.ChangeBugStatus(newStatusEnum);

            memberToChangeActivityHistoryFor
                .AddActivityHistoryToMember(bugToChangeStatus, teamToChangeStatusOfBoardIn, boardToChangeStatusIn, newStatusEnum);

            boardToChangeStatusIn
                .AddActivityHistoryToBoard(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            bugToChangeStatus
                .AddActivityHistoryToWorkItem(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            return string.Format(BugStatusChanged, bugToChangeStatusFor, newStatus);
        }
    }
}
