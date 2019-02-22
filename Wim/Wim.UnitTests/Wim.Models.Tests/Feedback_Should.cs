using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wim.Core.Engine;
using Wim.Models.Enums;

namespace Wim.UnitTests.Wim.Models.Tests
{
    [TestClass]
    public class Feedback_Should
    {
        [TestMethod]
        public void Constructor_Should_AssignCorrectTitle()
        {
            //Arange
            var factory = new WimFactory();
            var title = "ExampleStory";
            var descritpion = "Example Description";
            var rating = 5;

            //Act
            var sut = factory.CreateFeedback(title, descritpion, rating, FeedbackStatus.New);

            //Assert
            Assert.AreEqual(sut.Title, title);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectDescription()
        {
            //Arange
            var factory = new WimFactory();
            var title = "ExampleStory";
            var description = "Example Description";
            var rating = 5;

            //Act
            var sut = factory.CreateFeedback(title, description, rating, FeedbackStatus.New);

            //Assert
            Assert.AreEqual(sut.Description, description);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectRating()
        {
            //Arange
            var factory = new WimFactory();
            var title = "ExampleStory";
            var descritpion = "Example Description";
            var rating = 5;

            //Act
            var sut = factory.CreateFeedback(title, descritpion, rating, FeedbackStatus.New);

            //Assert
            Assert.AreEqual(sut.Rating, rating);


        }

    }
}
