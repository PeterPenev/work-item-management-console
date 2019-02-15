using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimCommandProcessor
    {
        IList<string> ProcessCommands(IList<ICommand> commands, IWimProcessSingleCommander processSingleCommander);
    }
}
