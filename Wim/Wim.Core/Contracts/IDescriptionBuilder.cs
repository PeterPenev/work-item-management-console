using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.Contracts
{
    public interface IDescriptionBuilder
    {
        string BuildDescription(IList<string> inputParameters, int startingIndex);

        string BuildDescription(IList<string> inputParameters, string delimator);
    }
}
