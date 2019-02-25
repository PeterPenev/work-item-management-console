using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IMember
    {
        string Name { get; }

        List<Guid> WorkItemsId { get; }

        List<IActivityHistory> ActivityHistory { get; }
    }
}
