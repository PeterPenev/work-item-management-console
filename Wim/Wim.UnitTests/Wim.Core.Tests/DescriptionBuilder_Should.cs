using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wim.Core.Engine;

namespace Wim.UnitTests.Wim.Core.Tests
{
    [TestClass]
    public class DescriptionBuilder_Should
    {
        [TestMethod]
        public void DescriptionBuilder_UsingStartingIndex()
        {
            //Arrange
            List<string> inputParameters = new List<string>() { "!Steps", "#1.", "open", "application", "#2.", "type", "command", "Create", "Bug", "#3.", "error", "message", "appear", "!Steps", "First description to the workitem to be shown!"};
            var startingIndex = 14;

            var descriptionBuilder = new DescriptionBuilder();

            var sut = descriptionBuilder.BuildDescription(inputParameters, startingIndex);

            Assert.AreEqual(sut, inputParameters[inputParameters.Count-1]);
        }
        
    }
}
