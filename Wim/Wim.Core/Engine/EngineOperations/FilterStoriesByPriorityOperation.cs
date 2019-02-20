using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterStoriesByPriorityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public FilterStoriesByPriorityOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
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
            var priorityToCheckFor = this.enumParser.GetPriority(priorityToFilterStoryFor);

            var filteredStoriesByPriority = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
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
