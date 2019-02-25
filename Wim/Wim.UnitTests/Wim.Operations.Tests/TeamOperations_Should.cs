using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wim.Models.Interfaces;
using Wim.Models.Operations;
using Wim.Models.Operations.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class TeamOperations_Should
    {
        [TestMethod]
        public void AddMember_ShouldReturnCorrectlyAddedMember()
        {
            //Arrange
            var mockMemberOperations = new Mock<IMemberOpertaions>();
            var teamOperations = new TeamOperations(mockMemberOperations.Object);
            var mockMember = new Mock<IMember>();
            mockMember.Setup(x => x.Name).Returns("Gosho");
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(x => x.Members).Returns(new List<IMember>());

            //Act
            teamOperations.AddMember(mockTeam.Object, mockMember.Object);

            //Assert
            Assert.AreSame(mockTeam.Object.Members.First(x => x.Name == "Gosho"), mockMember.Object);
        }
        
        [TestMethod]
        public void AddBoard_ShouldReturnCorrectlyAddedBoard()
        {
            //Arrange
            var mockMemberOperations = new Mock<IMemberOpertaions>();
            var teamOperations = new TeamOperations(mockMemberOperations.Object);
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Name).Returns("TestBoard");
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(x => x.Boards).Returns(new List<IBoard>());

            //Act
            teamOperations.AddBoard(mockTeam.Object, mockBoard.Object);

            //Assert
            Assert.AreSame(mockTeam.Object.Boards.First(x => x.Name == "TestBoard"), mockBoard.Object);
        }

        [TestMethod]
        public void Should_ShowAllTeamBoardsCorrectly()
        {
            //Arrange
            var mockTeam = new Mock<ITeam>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Name).Returns("Board01");
            mockTeam.Setup(x => x.Name).Returns("Team1");
            mockTeam.Setup(x => x.Boards).Returns(new List<IBoard>());
            mockTeam.Object.Boards.Add(mockBoard.Object);
            var mockMemberOperations = new Mock<IMemberOpertaions>();
            var teamOperations = new TeamOperations(mockMemberOperations.Object);

            //Act
            var sut = teamOperations.ShowAllTeamBoards(mockTeam.Object);

            Assert.AreEqual(sut, $"Team name: Team1\r\n1. Board01");
        }

        
    }
}
