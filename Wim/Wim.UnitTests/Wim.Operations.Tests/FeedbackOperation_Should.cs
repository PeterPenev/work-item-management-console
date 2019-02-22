using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class FeedbackOperation_Should
    {
        [TestMethod]
        public void ChangeFeedbackRating_Should()
        {
            //Arange
            var mockFeedback = new Mock<IFeedback>();
            mockFeedback.Setup(x => x.Rating).Returns(4);
            var feedbackOperations = new FeedbackOperations();

            //Act
            feedbackOperations.ChangeFeedbackRating(mockFeedback.Object, 6);

            //Assert
            Assert.AreEqual(mockFeedback.Object.Rating, 6);
        }

    }
}
