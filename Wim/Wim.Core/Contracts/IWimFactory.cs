using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimFactory
    {
        Team CreateTeam(string name);

        Person CreatePerson(string name);

        Member CreateMember(string name);

        Board CreateMember(string name);
    }
}
