using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IMember
    {
        List<WorkItem> WorkItems { get; set; }
        List<ActivityHistory> ActivityHistory { get; set; }
    }
}
