using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterBugsByPriorityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public FilterBugsByPriorityOperation(
            IBusinessLogicValidator businessLogicValidator,
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
            string priorityToFilterBugFor = inputParameters[0];

            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterBugFor, priorityTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var isPriorityEnumConvertable = Enum.TryParse(priorityToFilterBugFor, out Priority priorityToCheckFor);

            inputValidator.IsEnumConvertable(isPriorityEnumConvertable, "Priority");

            var filteredBugsByPriority = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();

            long workItemCounter = 1;

            if (filteredBugsByPriority.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {priorityToFilterBugFor} Priority in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL BUGS WITH {priorityToFilterBugFor} PRIORITY IN APPLICAITION----");
                foreach (var item in filteredBugsByPriority)
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
