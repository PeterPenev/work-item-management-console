using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Engine;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.UnitTests.Wim.Models.Tests
{
    [TestClass]
    public class Bug_Should
    {
        public void Constructor_Should_AssignsCorrectTitle()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleBug";
            var mockPerson = new Mock<IMember>();
            IList<string> stepsToReproduce = new List<string>() { "1. ExampleStepOne", "2.ExampleStepTwo" };
            var descritpion = "Example Description";

            //Act
            var sut = factory.CreateBug(title, Priority.High, Severity.Minor, mockPerson.Object, stepsToReproduce, descritpion);

            //Assert
            Assert.AreEqual(sut.Title, title);
        }
    }
}
