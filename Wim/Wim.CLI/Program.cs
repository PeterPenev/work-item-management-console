using System;
using Wim.Core.Engine;

namespace Wim.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            WimEngine.Instance.Start();
        }
    }
}
