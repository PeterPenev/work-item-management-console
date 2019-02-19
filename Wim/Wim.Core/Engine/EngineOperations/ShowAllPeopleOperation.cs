using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowAllPeopleOperation : IEngineOperations
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllMembers allMembers;

        public ShowAllPeopleOperation(
            IInputValidator inputValidator,
            IAllMembers allMembers)
        {
            this.inputValidator = inputValidator;
            this.allMembers = allMembers;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Validations
            inputValidator.ValdateIfAnyMembersExist(allMembers);

            //Operations
            var peopleToDisplay = allMembers.ShowAllMembersToString();

            return string.Format(peopleToDisplay);
        }
    }
}
