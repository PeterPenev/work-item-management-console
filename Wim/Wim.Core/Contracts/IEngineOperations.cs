using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IEngineOperations
    {
        string Execute(IList<string> inputParameters);
    }
}
