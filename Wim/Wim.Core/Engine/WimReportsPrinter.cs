using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimReportsPrinter : IWimReportsPrinter
    {
        public void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
        }
    }
}
