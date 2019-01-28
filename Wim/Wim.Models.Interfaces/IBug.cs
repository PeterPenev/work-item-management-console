using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IBug
    {
        Guid Id { get; set }
        List<string> StepsToReproduce { get; set; }
        Priority Priority { get; set; }
        Severity Severity { get; set; }
        Member Asignee { get; set; }
    }
}
