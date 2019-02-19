using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamsOperation : IEngineOperations
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public ShowAllTeamsOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }
        public string Execute(IList<string> inputParameters)
        {
            //Validations
            inputValidator.ValdateIfAnyTeamsExist(allTeams);

            //Operations
            var teamsToDisplay = allTeams.ShowAllTeamsToString();

            return string.Format(teamsToDisplay);
        }
    }
}
