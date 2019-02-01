using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IAllMembers
    {
        List<IMember> AllMembersList { get; }

        void AddMember(IMember member);
    }
}