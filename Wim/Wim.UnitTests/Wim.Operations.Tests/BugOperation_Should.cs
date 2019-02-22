using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;
using Wim.Models.Operations;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class BugOperation_Should
    {
        [TestMethod]
        public void ChangeBugPriority_Should()
        {
            //Arange
            var mockBug = new Mock<IBug>();
            mockBug.Setup(x => x.Priority).Returns(Priority.Low);
            var bugOperations = new BugOperations();

            //Act
            bugOperations.ChangeBugPriority(mockBug.Object, Priority.High);

            //Assert
            Assert.AreEqual(mockBug.Object.Priority, Priority.High);
        }
    }
}
