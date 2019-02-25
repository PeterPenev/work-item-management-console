using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IWimReportsPrinter
    {
        void PrintReports(IList<string> reports);
    }
}
