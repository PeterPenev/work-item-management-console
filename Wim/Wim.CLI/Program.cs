using System;
using Wim.Core.Engine;
using Wim.Core.Engine.EngineOperations;
using Wim.Models;

namespace Wim.CLI
{
    class Program
    {
        static void Main(string[] args)
        {    
            //Refactor with AutoFac
            var factory = new WimFactory();
            var allMembers = new AllMembers();
            var allTeams = new AllTeams();
            var enumParser = new EnumParser();
            var validator = new InputValidator();
            var commandHelper = new CommandHelper();
            var commandReader = new WimCommandReader();
            var commandProcessor = new WimCommandProcessor();            
            var reportsPrinter = new WimReportsPrinter();
            var inputValidator = new InputValidator();
            var changeOperations = new ChangeOperations(inputValidator, allTeams, allMembers, enumParser);
            var createOperations = new CreateOperations(inputValidator, allTeams, allMembers, enumParser, factory);
            var filterOperations = new FilterOperations(inputValidator, allTeams, allMembers, enumParser);
            var showOperations = new ShowOperations(inputValidator, allTeams, allMembers, enumParser);
            var sortOperations = new SortOperations(inputValidator, allTeams, allMembers, enumParser);

            var processSingleCommander = new WimProcessSingleCommander (
                changeOperations,
                createOperations, 
                filterOperations,
                showOperations, 
                sortOperations);

            var engine = new WimEngine(
                factory, 
                allMembers, 
                allTeams, 
                enumParser,
                validator, 
                commandHelper, 
                commandReader, 
                commandProcessor,
                processSingleCommander,
                reportsPrinter);

            engine.Start();
        }
    }
}
