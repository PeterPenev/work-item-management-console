using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public sealed class WimEngine : IEngine
    {    
        private readonly ICommandHelper commandHelper;
        private readonly IWimCommandReader commandReader;
        private readonly IWimCommandProcessor commandProcessor;
        private readonly IWimReportsPrinter reportsPrinter;
        private IConsoleWriter consoleWriter;

        public WimEngine(
            ICommandHelper commandHelper,
            IWimCommandReader commandReader,
            IWimCommandProcessor commandProcessor,
            IWimProcessSingleCommander processSingleCommander,
            IWimReportsPrinter reportsPrinter,
            IConsoleWriter consoleWriter)
        {
            this.commandHelper = commandHelper;
            this.commandReader = commandReader;
            this.commandProcessor = commandProcessor;
            this.reportsPrinter = reportsPrinter;
            this.consoleWriter = consoleWriter;
        }

        public void Start()
        {
            consoleWriter.WriteLine(commandHelper.Help);
            var commands = this.commandReader.ReadCommands();
            var commandResult = this.commandProcessor.ProcessCommands(commands);
            this.reportsPrinter.PrintReports(commandResult);
        }                             
    }
}
