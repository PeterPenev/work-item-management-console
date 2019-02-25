using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllTeamsOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllTeamsOperations allTeamsOperations;

        public ShowAllTeamsOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllTeamsOperations allTeamsOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allTeamsOperations = allTeamsOperations;
        }
        public string Execute(IList<string> inputParameters)
        {
            //Validations
            businessLogicValidator.ValdateIfAnyTeamsExist(allTeams);

            //Operations
            var teamsToDisplay = allTeamsOperations.ShowAllTeamsToString(allTeams);

            return string.Format(teamsToDisplay);
        }
    }
}
