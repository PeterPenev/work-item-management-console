﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterStoriesByStatusOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public FilterStoriesByStatusOperation(
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
            string statusToFilterStoryFor = inputParameters[0];
            
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterStoryFor, statusTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var isStatusEnumConvertable = Enum.TryParse(statusToFilterStoryFor, out StoryStatus storyStatusToCheckFor);

            inputValidator.IsEnumConvertable(isStatusEnumConvertable, "Status");

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
