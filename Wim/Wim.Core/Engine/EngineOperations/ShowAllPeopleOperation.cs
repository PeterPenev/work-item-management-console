using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllPeopleOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllMembers allMembers;

        public ShowAllPeopleOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllMembers allMembers)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allMembers = allMembers;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Validations
            businessLogicValidator.ValdateIfAnyMembersExist(allMembers);

            //Operations
            var peopleToDisplay = allMembers.ShowAllMembersToString();

            return string.Format(peopleToDisplay);
        }
    }
}
