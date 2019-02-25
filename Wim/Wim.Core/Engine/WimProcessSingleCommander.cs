using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimProcessSingleCommander : IWimProcessSingleCommander
    {
        private IWimCommandFinder wimCommandFinder;

        public WimProcessSingleCommander(IWimCommandFinder wimCommandFinder)
        {
            this.wimCommandFinder = wimCommandFinder;
        }

        public string ProcessSingleCommand(ICommand command)
        {
            var commandClassToExecuteFor = wimCommandFinder.FindSingleCommand(command);

            var result = commandClassToExecuteFor.Execute(command.Parameters);

            return result;           
        }
    }
}
