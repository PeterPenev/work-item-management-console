using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimCommandProcessor : IWimCommandProcessor
    {
        private readonly IWimProcessSingleCommander processSingleCommander;

        public WimCommandProcessor(IWimProcessSingleCommander processSingleCommander)
        {
            this.processSingleCommander = processSingleCommander;
        }

        public IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.processSingleCommander.ProcessSingleCommand(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }
    }
}
