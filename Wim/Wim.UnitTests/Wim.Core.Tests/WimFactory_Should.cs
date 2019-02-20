using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Engine;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.UnitTests.Wim.Core.Tests
{
    [TestClass]
    public class Factory_Should
    {
        [TestMethod]
        public void CreateBug_Should_ReturnCorrectInstance()
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
            Assert.IsInstanceOfType(sut, typeof(Bug));
        }

        [TestMethod]
        public void CreateStory_Should_ReturnCorrectInstance()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleStory";
            var mockPerson = new Mock<IMember>();
            var descritpion = "Example Description";

            //Act
            var sut = factory.CreateStory(title, descritpion, Priority.High, Size.Large, StoryStatus.InProgress, mockPerson.Object);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Story));
        }


        [TestMethod]
        public void CreateFeedback_Should_ReturnCorrectInstance()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleFeedback";
            var rating = 4;
            var descritpion = "Example Description";

            //Act
            var sut = factory.CreateFeedback(title, descritpion, rating, FeedbackStatus.New);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Feedback));
        }

        [TestMethod]
        public void CreateBoard_Should_ReturnCorrectInstance()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleBoard";

            //Act
            var sut = factory.CreateBoard(title);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Board));
        }

        [TestMethod]
        public void CreateMember_Should_ReturnCorrectInstance()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleMember";

            //Act
            var sut = factory.CreateMember(title);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Member));
        }

        [TestMethod]
        public void CreateTeam_Should_ReturnCorrectInstance()
        {
            //Arrange
            var factory = new WimFactory();
            var title = "ExampleMember";

            //Act
            var sut = factory.CreateTeam(title);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(Team));
        }
    }
}
