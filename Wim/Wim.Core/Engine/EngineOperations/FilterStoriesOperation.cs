using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    class FilterStoriesOperation : IEngineOperations
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public FilterStoriesOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var AllWorkItems = allTeams.AllTeamsList.Values
                    .SelectMany(x => x.Boards)
                        .SelectMany(x => x.WorkItems)
                            .Where(x => x.GetType() == typeof(Story))
                                .ToList();

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
    }
}
