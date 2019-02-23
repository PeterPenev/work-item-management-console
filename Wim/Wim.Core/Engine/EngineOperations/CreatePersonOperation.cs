using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreatePersonOperation : IEngineOperations
    {
        private const string PersonCreated = "Person with name {0} was created!";

        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IWimFactory factory;
        private readonly IAllMembersOperations allMembersOperations;

        public CreatePersonOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IWimFactory factory,
            IAllMembersOperations allMembersOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.factory = factory;
            this.allMembersOperations = allMembersOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            string personName = inputParameters[0];
            //Validations          
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personName, personTypeForChecking);

            inputValidator.ValdateMemberNameLength(personName);

            businessLogicValidator.ValidateIfPersonExists(allMembers, personName);

            //Operations
            var person = this.factory.CreateMember(personName, allTeams);
            allMembersOperations.AddMember(allMembers, person);

            return string.Format(PersonCreated, personName);
        }
    }
}
