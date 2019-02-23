using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimCommandReader : IWimCommandReader
    {
        private readonly IConsoleReader consoleReader;

        public WimCommandReader(IConsoleReader consoleReader)
        {
            this.consoleReader = consoleReader;
        }

        public IList<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentLine = consoleReader.ReadLine();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = Command.Parse(currentLine);
                commands.Add(currentCommand);

                currentLine = consoleReader.ReadLine();
            }

            return commands;
        }
    }
}
