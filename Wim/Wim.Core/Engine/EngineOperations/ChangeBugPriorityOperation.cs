﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    class ChangeBugPriorityOperation
    {
        private const string BugPriorityChanged = "Bug {0} priority is changed to {1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public ChangeBugPriorityOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
        }

        public string ChangeBugPriority(string teamToChangeBugPriorityFor, string boardToChangeBugPriorityFor, string bugToChangePriorityFor, string priority, string authorOfBugPriorityChange)
        {
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

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor, bugToChangePriorityFor);

            //Operations
            var newPriorityEnum = enumParser.GetPriority(priority);

            var itemType = "Bug";

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            castedBugForPriorityChange.ChangeBugPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugPriorityFor, authorOfBugPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugPriorityFor, itemType, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, priority);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, priority);

            bugToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, bugToAddActivityFor, priority);

            return string.Format(BugPriorityChanged, bugToChangePriorityFor, newPriorityEnum);
        }
    }
}