using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Engine;

namespace Wim.UnitTests.Wim.Core.Tests
{
    [TestClass]
    public class InputValidator_Should
    {
        [TestMethod]
        public void IsNullOrEmpty_Should_ThrowArgumentNullException_WhenInputIsNullOrEmpty()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act, Assert
            Assert.ThrowsException<ArgumentNullException>(() => inputValidator.IsNullOrEmpty(null, "SomeType"));                    
        }

        [TestMethod]
        public void IsNullOrEmpty_Should_ThrowCorrectArgumentNullExceptionMessage_WhenInputToCheckIsNullOrEmpty()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act
            var ex = Assert.ThrowsException<ArgumentNullException>(() => inputValidator.IsNullOrEmpty(null, "SomeType"));

            //Assert
            Assert.AreEqual(ex.Message, "Value cannot be null.\r\nParameter name: There are no Teams in the Application yet!");
        }
    }
}
