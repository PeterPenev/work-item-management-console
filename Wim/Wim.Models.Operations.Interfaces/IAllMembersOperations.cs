using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IAllMembersOperations
    {
        void AddMember(IAllMembers allMembers, IMember member);

        string ShowAllMembersToString(IAllMembers allMembers);
    }
}
