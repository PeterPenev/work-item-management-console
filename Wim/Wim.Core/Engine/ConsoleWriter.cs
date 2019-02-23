using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string inputForDisplay)
        {
            Console.WriteLine(inputForDisplay);
        }
    }
}
