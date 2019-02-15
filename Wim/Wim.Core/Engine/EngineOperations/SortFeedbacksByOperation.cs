using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class SortFeedbacksByOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public SortFeedbacksByOperation(
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

        public string SortFeedbacksBy(string factorToSortBy)
        {
            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
            var filteredFeedbacks = new List<Feedback>();
            if (factorToSortBy.ToLower() == "title")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "rating")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Rating)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.FeedbackStatus)
                                        .ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL FEEDBACKS IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredFeedbacks)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }
    }
}
