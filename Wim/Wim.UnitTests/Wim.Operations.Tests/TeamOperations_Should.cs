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
    }
}
