using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class AllMembersOperations : IAllMembersOperations
    {
        public void AddMember(IAllMembers allMembers, IMember member)
        {
            allMembers.AllMembersList.Add(member.Name, member);
        }

        public string ShowAllMembersToString(IAllMembers allMembers)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfPerson = 1;

            foreach (var item in allMembers.AllMembersList)
            {
                sb.AppendLine($"{numberOfPerson}. {item.Key}");
                numberOfPerson++;
            }

            return sb.ToString().Trim();
        }
    }
}
