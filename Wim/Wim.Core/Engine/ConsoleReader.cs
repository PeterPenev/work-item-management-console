using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class ConsoleReader : IConsoleReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
