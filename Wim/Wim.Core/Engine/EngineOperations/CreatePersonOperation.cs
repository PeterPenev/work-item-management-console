using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreatePersonOperation
    {
        private const string PersonCreated = "Person with name {0} was created!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IEnumParser enumParser;
        private readonly IWimFactory factory;

        public CreatePersonOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IEnumParser enumParser,
            IWimFactory factory)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.enumParser = enumParser;
            this.factory = factory;
        }

        public string CreatePerson(string personName)
        {
            //Validations          
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personName, personTypeForChecking);

            inputValidator.ValdateMemberNameLength(personName);

            inputValidator.ValidateIfPersonExists(allMembers, personName);

            //Operations
            var person = this.factory.CreateMember(personName);
            allMembers.AddMember(person);

            return string.Format(PersonCreated, personName);
        }
    }
}
