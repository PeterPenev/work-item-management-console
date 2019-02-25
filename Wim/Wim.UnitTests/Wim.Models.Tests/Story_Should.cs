using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wim.Core.Engine;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.UnitTests.Wim.Models.Tests
{
    [TestClass]
    public class Story_Should
    {
        [TestMethod]
        public void Constructor_Should_AssignsCorrectTitle()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.Title, storyTitle);
        }

        [TestMethod]
        public void Constructor_Should_AssignsCorrectPriority()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.Priority, storyPriority);
        }

        [TestMethod]
        public void Constructor_Should_AssignsCorrectSize()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.Size, storySize);
        }

        [TestMethod]
        public void Constructor_Should_AssignsCorrectStatus()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.StoryStatus, storyStatus);
        }

        [TestMethod]
        public void Constructor_Should_AssignsCorrectAssignee()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.Assignee, MockStoryAssignee.Object);
        }

        [TestMethod]
        public void Constructor_Should_AssignsCorrectDescription()
        {
            //Arrange
            var factory = new WimFactory();

            var storyTitle = "StoryTitleToCheck";
            var storyDescription = "This is the first story description";
            var storyPriority = Priority.High;
            var storySize = Size.Large;
            var storyStatus = StoryStatus.NotDone;
            var MockStoryAssignee = new Mock<IMember>();

            //Act
            var sut = factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyStatus, MockStoryAssignee.Object);

            //Assert
            Assert.AreEqual(sut.Description, storyDescription);
        }
    }
}
