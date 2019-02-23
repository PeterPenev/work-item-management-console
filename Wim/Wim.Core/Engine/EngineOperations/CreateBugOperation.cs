using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateBugOperation : IEngineOperations
    {
        private const string BugCreated = "Bug {0} was created!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;
        private readonly IDescriptionBuilder descriptionBuilder;
        private readonly IStepsToReproduceBuilder stepsToReproduceBuilder;
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IMemberOpertaions memberOpertaions;

        public CreateBugOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser,
            IWimFactory factory,
            IDescriptionBuilder descriptionBuilder,
            IStepsToReproduceBuilder stepsToReproduceBuilder,
            IBusinessLogicValidator businessLogicValidator,
            IMemberOpertaions memberOpertaions)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
            this.factory = factory;
            this.descriptionBuilder = descriptionBuilder;
            this.stepsToReproduceBuilder = stepsToReproduceBuilder;
            this.businessLogicValidator = businessLogicValidator;
            this.memberOpertaions = memberOpertaions;
        }
        public string Execute(IList<string> inputParameters)
        {
            string bugTitle = inputParameters[0];
            string teamToAddBugFor = inputParameters[1];
            string boardToAddBugFor = inputParameters[2];
            string bugPriority = inputParameters[3];
            string bugSeverity = inputParameters[4];
            string bugAssignee = inputParameters[5];

            IList<string> bugStepsToReproduce = stepsToReproduceBuilder.BuildStepsToReproduce(inputParameters, "!Steps");

            var bugDescription = descriptionBuilder.BuildDescription(inputParameters, "!Steps");

            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugTitle, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddBugFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddBugFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(bugTitle);

            inputValidator.ValdateItemDescriptionLength(bugDescription);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToAddBugFor);

            businessLogicValidator.ValidateMemberExistance(allMembers, bugAssignee);

            businessLogicValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddBugFor, bugAssignee);

            businessLogicValidator.ValidateBugExistanceInBoard(allTeams, boardToAddBugFor, teamToAddBugFor, bugTitle);

            //Operations
            var isPriorityEnumConvertable = Enum.TryParse(bugPriority, out Priority bugPriorityEnum);
            inputValidator.IsEnumConvertable(isPriorityEnumConvertable, "Priority");

            var isSeverityEnumConvertable = Enum.TryParse(bugSeverity, out Severity bugSeverityEnum);
            inputValidator.IsEnumConvertable(isSeverityEnumConvertable, "Severity");

            IBug bugToAddToCollection = this.factory.CreateBug(bugTitle, bugPriorityEnum, bugSeverityEnum, allMembers.AllMembersList[bugAssignee], bugStepsToReproduce, bugDescription);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddBugFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddBugFor);

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(bugToAddToCollection);

            var memberToTrackActivityFor = allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee);

            memberOpertaions.AddWorkItemIdToMember(memberToTrackActivityFor, bugToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam];
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor];

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToTrackActivityFor, bugToAddToCollection);
            memberOpertaions.AddActivityHistoryToMember(memberToTrackActivityFor, bugToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(BugCreated, bugTitle);
        }
    }
}
