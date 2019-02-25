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

        [TestMethod]
        public void Should_AddActivityHistoryToBoardPlusMember()
        {
            //Arrange
            var boardOperations = new BoardOperations();
            var mockBoard = new Mock<IBoard>();
            var mockWorkItem = new Mock<IBug>();
            var mockMember = new Mock<IMember>();
            mockBoard.Setup(x => x.WorkItems).Returns(new List<IWorkItem>());
            mockWorkItem.Setup(x => x.Title).Returns("Bug1234567");
            mockMember.Setup(x => x.Name).Returns("Edward");
            mockBoard.Setup(x => x.ActivityHistory).Returns(new List<IActivityHistory>());

            //Act
            boardOperations.AddActivityHistoryToBoard(mockBoard.Object, mockMember.Object, mockWorkItem.Object);

            //Assert
            Assert.AreEqual(mockBoard.Object.ActivityHistory.First().Message, "A IBugProxy with Title: Bug1234567 was created by Member: Edward");
        }

        [TestMethod]
        public void Should_AddActivityHistoryToBoardForAssignAndUnassign()
        {
            //Arrange
            var boardOperations = new BoardOperations();
            var mockBoard = new Mock<IBoard>();
            var mockMemberAssign = new Mock<IMember>();
            var mockMemberUnassign = new Mock<IMember>();
            mockBoard.Setup(x => x.WorkItems).Returns(new List<IWorkItem>());
            mockMemberAssign.Setup(x => x.Name).Returns("Edward");
            mockMemberUnassign.Setup(x => x.Name).Returns("Kiro");
            mockBoard.Setup(x => x.ActivityHistory).Returns(new List<IActivityHistory>());
            string type = "bug";
            string workItemTitle = "bug1234567";

            //Act
            boardOperations.AddActivityHistoryAfterAssignUnsignToBoard(mockBoard.Object, type, workItemTitle, mockMemberAssign.Object, mockMemberUnassign.Object);

            //Assert
            Assert.AreEqual(mockBoard.Object.ActivityHistory.First().Message, "A bug with Title: bug1234567 was unassigned from Kiro and assigned to Edward");
        }
    }
}
