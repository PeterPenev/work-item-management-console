using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IStepsToReproduceBuilder
    {
        IList<string> BuildStepsToReproduce(IList<string> inputParameters, string delimator);
    }
}
