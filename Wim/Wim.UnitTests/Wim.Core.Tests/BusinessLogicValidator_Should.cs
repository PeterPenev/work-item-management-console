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
    }
}
