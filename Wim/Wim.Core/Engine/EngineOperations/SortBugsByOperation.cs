using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class SortBugsByOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public SortBugsByOperation(IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Assign Values From List Of Parameters
            string factorToSortBy = inputParameters[0];

            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var filteredBugs = new List<Bug>();
            if (factorToSortBy.ToLower() == "title")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "priority")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Priority)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "severity")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Severity)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.BugStatus)
                                        .ToList();
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL BUGS IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredBugs)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title}");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }
    }
}
