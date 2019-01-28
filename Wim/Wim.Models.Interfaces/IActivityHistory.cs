using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IActivityHistory
    {
        Guid Id { get; }
        string Message { get; }
        DateTime LoggingDate { get; }
    }
}
