using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class DescriptionBuilder : IDescriptionBuilder
    {
        public string BuildDescription(IList<string> inputParameters, int startingIndex)
        {
            var buildDescription = new StringBuilder();

            for (int i = startingIndex; i < inputParameters.Count; i++)
            {
                buildDescription.Append(inputParameters[i] + " ");
            }
            string resultedDescription = buildDescription.ToString().Trim();
            return resultedDescription;
        }

        public string BuildDescription(IList<string> inputParameters, string delimator)
        {
            var buildDescription = new StringBuilder();

            var stepsAndDescription = string.Join(' ', inputParameters);
            var stepsAndDescriptionArr = stepsAndDescription.Split("!Steps").ToArray();
            var description = stepsAndDescriptionArr[2].ToString().Trim();
            return description;            

        }
    }
}
