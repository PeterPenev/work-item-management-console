using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public sealed class CommandHelper : ICommandHelper
    {
        private string help;

        public CommandHelper()
        {
            this.Help = this.FillHelpFromFile();
        }

        public string Help
        {
            get
            {
                return this.help;
            }
            private set
            {
                this.help = value;
            }
        }

        private string FillHelpFromFile()
        {
            StringBuilder Sb = new StringBuilder();
            using (StreamReader Reader = new StreamReader("..\\..\\..\\..\\..\\Readme.md"))
            {

                Sb.Append(Reader.ReadToEnd());
            }
            return Sb.ToString().Trim();
        }
    }
}
