using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimCommandFinder : IWimCommandFinder
    {
        private readonly IComponentContext componentContext;

        public WimCommandFinder(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IEngineOperations FindSingleCommand(ICommand command)
        {
            var engineOperation = this.componentContext.ResolveNamed<IEngineOperations>(command.Name);
            return engineOperation;
        }
    }
}
