using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.CustomExceptions;
using Wim.Core.Engine;
using Wim.Models.Enums;

namespace Wim.UnitTests.Wim.Core.Tests
{
    [TestClass]
    public class InputValidator_Should
    {
        [TestMethod]
        public void IsNullOrEmpty_Should_ThrowArgumentNullException_WhenInputIsNull()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act, Assert
            Assert.ThrowsException<ArgumentNullException>(() => inputValidator.IsNullOrEmpty(null, "SomeType"));
        }

        [TestMethod]
        public void IsNullOrEmpty_Should_ThrowArgumentNullException_WhenInputIsEmpty()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act, Assert
            Assert.ThrowsException<ArgumentNullException>(() => inputValidator.IsNullOrEmpty(string.Empty, "SomeType"));
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

        [TestMethod]
        public void IsNullOrEmpty_Should_ThrowCorrectArgumentNullExceptionMessage_WhenInputToCheckIsEmpty()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act
            var ex = Assert.ThrowsException<ArgumentNullException>(() => inputValidator.IsNullOrEmpty(string.Empty, "SomeType"));

            //Assert
            Assert.AreEqual(ex.Message, "Value cannot be null.\r\nParameter name: There are no Teams in the Application yet!");
        }

        [TestMethod]
        public void IsEnumConvertable_Should_ThrowArgumentException_WhenInputIsFalse()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => inputValidator.IsEnumConvertable(false, Priority.High));
        }

        [TestMethod]
        public void IsEnumConvertable_Should_ThrowCorrectArgumentNullExceptionMessage_WhenInputIsFalse()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(() => inputValidator.IsEnumConvertable(false, Priority.High));

            //Assert
            Assert.AreEqual(ex.Message, "The Priority is not valid!");
        }

        [TestMethod]
        public void ValdateMemberNameLength_Should_ThrowImproperMemberNameLengthException_WhenInputIsTooShort()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act, Assert
            Assert.ThrowsException<ImproperMemberNameLengthException>(() => inputValidator.ValdateMemberNameLength("sa"));
        }

        [TestMethod]
        public void ValdateMemberNameLength_Should_ThrowCorrenctImproperMemberNameLengthExceptionMessage_WhenInputIsTooShort()
        {
            //Arrange
            var inputValidator = new InputValidator();

            //Act
            var ex = Assert.ThrowsException<ImproperMemberNameLengthException>(() => inputValidator.ValdateMemberNameLength("sa"));

            //Assert
            Assert.AreEqual(ex.Message, "Member name should be between 5 and 15 symbols!");
        }

        [TestMethod]
        public void ValdateMemberNameLength_Should_ThrowImproperMemberNameLengthException_WhenInputIsTooLong()
        {
            //Arrange
            var inputValidator = new InputValidator();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 20; i++)
            {
                sb.Append("s");
            }

            //Act, Assert
            Assert.ThrowsException<ImproperMemberNameLengthException>(() => inputValidator.ValdateMemberNameLength(sb.ToString().Trim()));
        }

        [TestMethod]
        public void ValdateMemberNameLength_Should_ThrowCorrenctImproperMemberNameLengthExceptionMessage_WhenInputIsTooLong()
        {
            //Arrange
            var inputValidator = new InputValidator();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 20; i++)
            {
                sb.Append("s");
            }

            //Act
            var ex = Assert.ThrowsException<ImproperMemberNameLengthException>(() => inputValidator.ValdateMemberNameLength(sb.ToString().Trim()));

            //Assert
            Assert.AreEqual(ex.Message, "Member name should be between 5 and 15 symbols!");
        }
    }
}