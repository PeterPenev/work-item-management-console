using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterStoriesByStatusOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;

        public FilterStoriesByStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
        }

        public string FilterStoriesByStatus(string statusToFilterStoryFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterStoryFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var storyStatusToCheckFor = this.enumParser.GetStoryStatus(statusToFilterStoryFor);

            var filteredStoriesbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.StoryStatus == storyStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredStoriesbyStatus.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {statusToFilterStoryFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES WITH {statusToFilterStoryFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredStoriesbyStatus)
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
