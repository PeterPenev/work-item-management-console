using System;
using Wim.Core.Engine;
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


            var engine = new WimEngine(factory, allMembers, allTeams, enumParser, validator, commandHelper, commandReader);
            engine.Start();
        }
    }
}
