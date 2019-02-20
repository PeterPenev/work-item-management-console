using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeBugStatusOperation : IEngineOperations
    {
        private const string BugStatusChanged = "Bug {0} status is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IBugOperations bugOperations;
        private readonly IBusinessLogicValidator businessLogicValidator;

        public ChangeBugStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IBugOperations bugOperations,
            IBusinessLogicValidator businessLogicValidator)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.bugOperations = bugOperations;
            this.businessLogicValidator = businessLogicValidator;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeBugStatusFor = inputParameters[0];
            string boardToChangeBugStatusFor = inputParameters[1];
            string bugToChangeStatusFor = inputParameters[2];
            string newStatus = inputParameters[3];
            string authorOfBugStatusChange = inputParameters[4];

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

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToChangeBugStatusFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor);

            businessLogicValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor, bugToChangeStatusFor);

            //Operations
            var itemType = "Bug";

            var isEnumConvertable = Enum.TryParse(newStatus, out BugStatus newStatusEnum);

            inputValidator.IsEnumConvertable(isEnumConvertable, "Status");

            var castedBugToChangeStatusIn = allTeams.FindBugAndCast(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var bugToChangeStatus = allTeams.FindWorkItem(teamToChangeBugStatusFor, itemType, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var boardToChangeStatusIn = allTeams.FindBoardInTeam(teamToChangeBugStatusFor, boardToChangeBugStatusFor);

            var teamToChangeStatusOfBoardIn = allTeams.AllTeamsList[teamToChangeBugStatusFor];

            var memberToChangeActivityHistoryFor = allTeams.FindMemberInTeam(teamToChangeBugStatusFor, authorOfBugStatusChange);

            bugOperations.ChangeBugStatus(castedBugToChangeStatusIn, newStatusEnum);

            memberToChangeActivityHistoryFor
                .AddActivityHistoryToMember(bugToChangeStatus, teamToChangeStatusOfBoardIn, boardToChangeStatusIn, newStatusEnum);

            boardToChangeStatusIn
                .AddActivityHistoryToBoard(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            bugOperations.AddActivityHistoryToWorkItem(bugToChangeStatus, memberToChangeActivityHistoryFor, newStatusEnum);

            return string.Format(BugStatusChanged, bugToChangeStatusFor, newStatus);
        }
    }
}
