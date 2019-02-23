using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllPeopleOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllMembers allMembers;
        private readonly IAllMembersOperations allMembersOpertaions;

        public ShowAllPeopleOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllMembers allMembers,
            IAllMembersOperations allMembersOpertaions)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allMembers = allMembers;
            this.allMembersOpertaions = allMembersOpertaions;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Validations
            businessLogicValidator.ValdateIfAnyMembersExist(allMembers);

            //Operations
            var peopleToDisplay = allMembersOpertaions.ShowAllMembersToString(allMembers);

            return string.Format(peopleToDisplay);
        }
    }
}
