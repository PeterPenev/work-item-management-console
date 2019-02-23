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
    }
}
