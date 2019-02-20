using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterStoriesByAssigneeOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public FilterStoriesByAssigneeOperation(IBusinessLogicValidator businessLogicValidator,
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
            string assigneeToFilterStoryFor = inputParameters[0];
            
            //Validations
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterStoryFor, assigneeTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var filteredStoriesByAssignee = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.Assignee.Name == assigneeToFilterStoryFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredStoriesByAssignee.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {assigneeToFilterStoryFor} Assignee in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES ASSIGNED TO MEMBER: {assigneeToFilterStoryFor} IN APPLICAITION----");
                foreach (var item in filteredStoriesByAssignee)
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
