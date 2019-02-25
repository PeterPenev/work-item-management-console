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
    public class FilterFeedbacksByStatusOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public FilterFeedbacksByStatusOperation(
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
            string statusToFilterFeedbacksFor = inputParameters[0];

            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterFeedbacksFor, statusTypeForChecking);

            businessLogicValidator.ValidateIfAnyWorkItemsExist(allTeams);

            businessLogicValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
            var isStatusEnumConvertable = Enum.TryParse(statusToFilterFeedbacksFor, out FeedbackStatus feedbacksStatusToCheckFor);

            inputValidator.IsEnumConvertable(isStatusEnumConvertable, "Status");

            var filteredFeedbacksbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                .Where(story => story.FeedbackStatus == feedbacksStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredFeedbacksbyStatus.Count == 0)
            {
                sb.AppendLine($"There are No Feedbacks with Staus {statusToFilterFeedbacksFor} in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL FEEDBACKS WITH {statusToFilterFeedbacksFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredFeedbacksbyStatus)
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
