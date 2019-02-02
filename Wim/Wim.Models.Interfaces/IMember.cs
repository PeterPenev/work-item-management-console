using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IMember
    {
        string Name { get; }

        bool IsAssigned { get; set; }

        List<IWorkItem> WorkItems { get; }

        List<IActivityHistory> ActivityHistory { get; }

        string ShowMemberActivityToString(IList<IActivityHistory> activityHistoryInput);
    }
}
