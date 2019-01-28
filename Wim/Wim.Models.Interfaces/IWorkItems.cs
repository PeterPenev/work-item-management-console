using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IWorkItems
    {
        string Title { get; set; }
        string Description { get; set; }
        List<string> Comments { get; set; }
        List<string> History { get; set; }
    }
}
