using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IAllMembers
    {
        List<IMember> AllMembers { get; }
    }
}