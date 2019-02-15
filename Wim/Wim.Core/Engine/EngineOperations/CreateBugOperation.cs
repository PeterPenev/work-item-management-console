using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateBugOperation
    {
        private const string BugCreated = "Bug {0} was created!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;

        public CreateBugOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser,
            IWimFactory factory)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
            this.factory = factory;
        }
        public string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAssignee, IList<string> bugStepsToReproduce, string bugDescription)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugTitle, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddBugFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddBugFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(bugTitle);

            inputValidator.ValdateItemDescriptionLength(bugDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddBugFor);

            inputValidator.ValidateMemberExistance(allMembers, bugAssignee);

            inputValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddBugFor, bugAssignee);

            inputValidator.ValidateBugExistanceInBoard(allTeams, boardToAddBugFor, teamToAddBugFor, bugTitle);

            //Operations
            Priority bugPriorityEnum = this.enumParser.GetPriority(bugPriority);
            Severity bugSeverityEnum = this.enumParser.GetSeverity(bugSeverity);
            IBug bugToAddToCollection = this.factory.CreateBug(bugTitle, bugPriorityEnum, bugSeverityEnum, allMembers.AllMembersList[bugAssignee], bugStepsToReproduce, bugDescription);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddBugFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddBugFor);

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(bugToAddToCollection);

            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddWorkItemIdToMember(bugToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor];

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, bugToAddToCollection);
            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddActivityHistoryToMember(bugToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(BugCreated, bugTitle);
        }
    }
}
