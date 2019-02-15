using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimProcessSingleCommander
    {
        string ProcessSingleCommand(ICommand command);
    }
}
