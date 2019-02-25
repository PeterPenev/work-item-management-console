using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class AllTeamsOperations_Should
    {
        [TestMethod]
        public void AddTeam_ShouldReturnCorrectlyAddedTeam()
        {
            //Arrange
            var allTeamsOperation = new AllTeamsOperations();
            var mockAllTeams = new Mock<IAllTeams>();
            var mockTeam = new Mock<ITeam>();
            mockTeam.Setup(x => x.Name).Returns("Alpha");
            mockAllTeams.Setup(x => x.AllTeamsList).Returns(new Dictionary<string, ITeam>());

            //Act
            allTeamsOperation.AddTeam(mockAllTeams.Object, mockTeam.Object);

            //Assert
            Assert.AreSame(mockAllTeams.Object.AllTeamsList["Alpha"], mockTeam.Object);
        }

        [TestMethod]
        public void Constructor_ShouldReturnCorrectDictionaryOfTeams()
        {
            //Arrange, Act
            var allTeams = new AllTeams();

            //Assert
            Assert.IsInstanceOfType(allTeams.AllTeamsList, new Dictionary<string, ITeam>().GetType());
        }

    }
}
