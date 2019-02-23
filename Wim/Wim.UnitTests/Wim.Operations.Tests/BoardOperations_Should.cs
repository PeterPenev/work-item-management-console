using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class BoardOperations_Should
    {
        [TestMethod]
        public void Should_AddWorkItemToBoard()
        {
            //Arrange
            var boardOperations = new BoardOperations();
            var mockBoard = new Mock<IBoard>();
            var mockWorkitem = new Mock<IBug>();
            mockBoard.Setup(x => x.WorkItems).Returns(new List<IWorkItem>());

            //Act
            boardOperations.AddWorkitemToBoard(mockBoard.Object, mockWorkitem.Object);

            //Assert
            Assert.IsTrue(mockBoard.Object.WorkItems.Contains(mockWorkitem.Object));
        }

        [TestMethod]
        public void Should_AddActivityHistoryToBoard()
        {
            //Arrange
            var boardOperations = new BoardOperations();
            var mockBoard = new Mock<IBoard>();
            var mockWorkItem = new Mock<IBug>();
            mockBoard.Setup(x => x.WorkItems).Returns(new List<IWorkItem>());
            mockWorkItem.Setup(x => x.Title).Returns("Bug1234567");
            mockBoard.Setup(x => x.ActivityHistory).Returns(new List<IActivityHistory>());

            //Act
            boardOperations.AddActivityHistoryToBoard(mockBoard.Object, mockWorkItem.Object);

            //Assert
            Assert.AreEqual(mockBoard.Object.ActivityHistory.First().Message, "A IBugProxy with Title: Bug1234567");
        }
    }
}
