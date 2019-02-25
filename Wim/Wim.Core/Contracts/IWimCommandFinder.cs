using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimCommandFinder
    {
        IEngineOperations FindSingleCommand(ICommand command);
    }
}
