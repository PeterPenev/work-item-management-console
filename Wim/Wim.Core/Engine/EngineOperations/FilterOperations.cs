using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.Engine.EngineOperationsContracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class FilterOperations : IFilterOperations
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;

        public FilterOperations(
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

        public string FilterBugs()
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Bug)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL BUGS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        public string FilterStories()
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Story)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL STORIES IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        public string FilterFeedbacks()
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Feedback)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL FEEDBACKS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        public string FilterBugsByPriority(string priorityToFilterBugFor)
        {
            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterBugFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var priorityToCheckFor = this.enumParser.GetPriority(priorityToFilterBugFor);

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

        public string FilterBugsByStatus(string statusToFilterBugFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterBugFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var bugStatusToCheckFor = this.enumParser.GetBugStatus(statusToFilterBugFor);

            var filteredBugsByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.BugStatus == bugStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            long workItemCounter = 1;

            if (filteredBugsByStatus.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {statusToFilterBugFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL BUGS WITH {statusToFilterBugFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredBugsByStatus)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        public string FilterStoriesByPriority(string priorityToFilterStoryFor)
        {
            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterStoryFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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

        public string FilterStoriesByAssignee(string assigneeToFilterStoryFor)
        {
            //Validations
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterStoryFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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

        public string FilterFeedbacksByStatus(string statusToFilterFeedbacksFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterFeedbacksFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
            var feedbacksStatusToCheckFor = this.enumParser.GetFeedbackStatus(statusToFilterFeedbacksFor);

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
