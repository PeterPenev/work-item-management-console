﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimCommandProcessor : IWimCommandProcessor
    {
        public IList<string> ProcessCommands(IList<ICommand> commands, IWimProcessSingleCommander processSingleCommander)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = processSingleCommander.ProcessSingleCommand(command);
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