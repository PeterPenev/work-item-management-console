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
    public class FilterStoriesByPriorityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public FilterStoriesByPriorityOperation(
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
            string priorityToFilterStoryFor = inputParameters[0];
            
            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterStoryFor, priorityTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var isStatusEnumConvertable = Enum.TryParse(priorityToFilterStoryFor, out Priority priorityToCheckFor);

            inputValidator.IsEnumConvertable(isStatusEnumConvertable, "Priority");

            var filteredStoriesByPriority = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredStoriesByPriority.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {priorityToFilterStoryFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES WITH {priorityToFilterStoryFor} PRIORITY IN APPLICAITION----");
                foreach (var item in filteredStoriesByPriority)
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
