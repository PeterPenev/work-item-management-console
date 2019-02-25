using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class StepsToReproduceBuilder : IStepsToReproduceBuilder
    {
        public IList<string> BuildStepsToReproduce(IList<string> inputParameters, string delimator)
        {
            var buildDescription = new StringBuilder();

            var stepsAndDescription = string.Join(' ', inputParameters);
            var stepsAndDescriptionArr = stepsAndDescription.Split("!Steps").ToArray();
            var steps = stepsAndDescriptionArr[1].Split('#').ToList();
            return steps;
        }
    }
}
