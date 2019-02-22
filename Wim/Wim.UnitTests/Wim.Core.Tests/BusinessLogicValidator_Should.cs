using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.CustomExceptions;
using Wim.Core.Engine;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.UnitTests.Wim.Core.Tests
{
    [TestClass]
    public class BusinessLogicValidator_Should
    {
        [TestMethod]
        public void ValdateIfAnyTeamsExist_Should_ThrowNoTeamsInAppException_WhenNoTeamsInApp()
        {
            //Arrange
            var mockAllTeams = new Mock<IAllTeams>();
            mockAllTeams.Setup(x => x.AllTeamsList).Returns(new Dictionary<string, ITeam>());

            var businessLogicValidator = new BusinessLogicValidator();

            //Act, Assert
            Assert.ThrowsException<NoTeamsInAppException>(() => businessLogicValidator.ValdateIfAnyTeamsExist(mockAllTeams.Object));
        }

        [TestMethod]
        public void ValdateIfAnyTeamsExist_Should_ThrowCorrectNoTeamsInAppExceptionMessage_WhenNoTeamsInApp()
        {
            //Arrange
            var mockAllTeams = new Mock<IAllTeams>();
            mockAllTeams.Setup(x => x.AllTeamsList).Returns(new Dictionary<string, ITeam>());

            var businessLogicValidator = new BusinessLogicValidator();

            //Act
            var ex = Assert.ThrowsException<NoTeamsInAppException>(() => businessLogicValidator.ValdateIfAnyTeamsExist(mockAllTeams.Object));

            //Assert
            Assert.AreEqual(ex.Message, "There are no Teams in the Application yet!");
        }

        [TestMethod]
        public void ValidateIfBoardExistsInTeam_Should_ThrowException()
        {
            //Arange
            var mockAllTeams = new Mock<IAllTeams>();
            mockAllTeams.Setup(x => x.AllTeamsList).Returns(new Dictionary<string,ITeam>());
            var mockTeam = new Mock<ITeam>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Name).Returns("Board01");
            mockTeam.Setup(x => x.Name).Returns("Alpha");

            mockTeam.Setup(x => x.Boards).Returns(new List<IBoard>());
            mockTeam.Object.Boards.Add(mockBoard.Object);
            mockAllTeams.Object.AllTeamsList.Add("Alpha", mockTeam.Object);
            

            var businesLogicValidator = new BusinessLogicValidator();

            //Act
            var ex = Assert.ThrowsException<BoardAlreadyExistsInTeamException>(() => businesLogicValidator.ValidateBoardAlreadyInTeam(mockAllTeams.Object, mockBoard.Object.Name, mockTeam.Object.Name));

            //Assert
            Assert.AreEqual(ex.Message, "Board with name Board01 already exists in team Alpha!");
        }
    }
}
