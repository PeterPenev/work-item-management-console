using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowPersonsActivityOperation : IEngineOperations
    {
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllMembers allMembers;
        private readonly IMemberOpertaions memberOperations;

        public ShowPersonsActivityOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllMembers allMembers,
            IMemberOpertaions memberOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allMembers = allMembers;
            this.memberOperations = memberOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            //Assign Values From List Of Parameters
            string memberName = inputParameters[0];

            //Validations
            var inputTypeForChecking = "Member Name";
            inputValidator.IsNullOrEmpty(memberName, inputTypeForChecking);

            businessLogicValidator.ValidateMemberExistance(allMembers, memberName);

            //Operations
            var selectedMember = this.allMembers.AllMembersList[memberName];

            var memberActivities = memberOperations.ShowMemberActivityToString(selectedMember);            

            return string.Format(memberActivities);
        }
    }
}
