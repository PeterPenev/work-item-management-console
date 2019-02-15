using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterBugsByAssigneeOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public FilterBugsByAssigneeOperation(
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

        public string FilterBugsByAssignee(string assigneeToFilterBugFor)
        {
            //Validations
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterBugFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var filteredBugsByAssignee = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Assignee.Name == assigneeToFilterBugFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredBugsByAssignee.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {assigneeToFilterBugFor} Assignee in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL BUGS ASSIGNED TO MEMBER: {assigneeToFilterBugFor} IN APPLICAITION----");
                foreach (var item in filteredBugsByAssignee)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }
    }
}
