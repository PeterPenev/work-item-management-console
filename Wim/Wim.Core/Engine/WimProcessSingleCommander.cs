using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.Engine.EngineOperationsContracts;

namespace Wim.Core.Engine
{
    public class WimProcessSingleCommander : IWimProcessSingleCommander
    {
        public string ProcessSingleCommand(ICommand command, IComponentContext componentContext)
        {
            var commandClassToExecuteFor = componentContext.ResolveNamed<IEngineOperations>(command.Name);

            var result = commandClassToExecuteFor.Execute(command.Parameters);

            return result;           
        }
    }
}
