using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IMember
    {
        string Name { get; }

        List<IWorkItem> WorkItems { get; }

        List<IActivityHistory> ActivityHistory { get; }

        string ShowMemberActivityToString(IList<IActivityHistory> activityHistoryInput);

        bool FindIfMemberIsAssigned(IDictionary<string, ITeam> allTeamsInput);
    }
}
