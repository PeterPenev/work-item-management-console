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
        private readonly IWimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IInputValidator inputValidator;
        private readonly ICommandHelper commandHelper;
        private readonly IWimCommandReader commandReader;
        private readonly IWimCommandProcessor commandProcessor;
        private readonly IWimProcessSingleCommander processSingleCommander;
        private readonly IWimReportsPrinter reportsPrinter;

        public WimEngine(IWimFactory factory, 
            IAllMembers allMembers, 
            IAllTeams allTeams, 
            EnumParser enumParser, 
            IInputValidator inputValidator, 
            ICommandHelper commandHelper,
            IWimCommandReader commandReader,
            IWimCommandProcessor commandProcessor,
            IWimProcessSingleCommander processSingleCommander,
            IWimReportsPrinter reportsPrinter)
        {
            this.factory = factory;
            this.allMembers = allMembers;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.inputValidator = inputValidator;
            this.commandHelper = commandHelper;
            this.commandReader = commandReader;
            this.commandProcessor = commandProcessor;
            this.processSingleCommander = processSingleCommander;
            this.reportsPrinter = reportsPrinter;
        }

        public void Start()
        {
            Console.WriteLine(commandHelper.Help);
            var commands = this.commandReader.ReadCommands();
            var commandResult = this.commandProcessor.ProcessCommands(commands, processSingleCommander);
            this.reportsPrinter.PrintReports(commandResult);
        }                             
    }
}
