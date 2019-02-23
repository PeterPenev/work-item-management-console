using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class StoryOperation_Should
    {
        [TestMethod]
        public void ChangeStoryPriority()
        {
            //Arrange
            var mockStory = new Mock<IStory>();
            var storyPriority = Priority.High;
            mockStory.Setup(x => x.Priority).Returns(Priority.Low);

            var storyOperations = new StoryOperations();

            //Act
            storyOperations.ChangeStoryPriority(mockStory.Object, storyPriority);

            //Assert
            Assert.AreEqual(storyPriority, mockStory.Object.Priority);
        }
    }
}
