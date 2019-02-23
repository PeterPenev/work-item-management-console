using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeBugPriorityOperation : IEngineOperations
    {
        private const string BugPriorityChanged = "Bug {0} priority is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IBugOperations bugOperations;
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IMemberOpertaions memberOpertaions;

        public ChangeBugPriorityOperation(
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
            //Assign Values
            string teamToChangeBugPriorityFor = inputParameters[0];
            string boardToChangeBugPriorityFor = inputParameters[1];
            string bugToChangePriorityFor = inputParameters[2];
            string priority = inputParameters[3];
            string authorOfBugPriorityChange = inputParameters[4];        
        
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangePriorityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugPriorityChange, authorTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToChangeBugPriorityFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor);

            businessLogicValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor, bugToChangePriorityFor);

            //Operations

            var isEnumConvertable = Enum.TryParse(priority, out Priority newPriorityEnum);

            inputValidator.IsEnumConvertable(isEnumConvertable, "Priority");

            var itemType = "Bug";

            var castedBsugToAddActivityFor = allTeams.FindBugAndCast(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            bugOperations.ChangeBugPriority(castedBsugToAddActivityFor, newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugPriorityFor, authorOfBugPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugPriorityFor, itemType, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, priority);

            memberOpertaions.AddActivityHistoryToMember(memberToAddActivityFor, bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, priority);

            bugOperations.AddActivityHistoryToWorkItem(bugToAddActivityFor, memberToAddActivityFor, priority);

            return string.Format(BugPriorityChanged, bugToChangePriorityFor, newPriorityEnum);
        }
    }
}
