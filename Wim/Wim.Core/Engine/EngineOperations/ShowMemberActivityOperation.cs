using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ShowMemberActivityOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllMembers allMembers;

        public ShowMemberActivityOperation(
            IInputValidator inputValidator,
            IAllMembers allMembers)
        {
            this.inputValidator = inputValidator;
            this.allMembers = allMembers;
        }

        public string ShowMemberActivityToString(string memberName)
        {
            //Validations
            var inputTypeForChecking = "Member Name";
            inputValidator.IsNullOrEmpty(memberName, inputTypeForChecking);

            inputValidator.ValidateMemberExistance(allMembers, memberName);

            //Operations
            var selectedMember = this.allMembers.AllMembersList[memberName];
            var memberActivities = selectedMember.ShowMemberActivityToString();

            return string.Format(memberActivities);
        }
    }
}
