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
    public class AllMembersOperations_Should
    {
        [TestMethod]
        public void AddMember_ShouldReturnCorrectlyAddedPerson()
        {
            //Arrange
            var allMembersOperation = new AllMembersOperations();
            var mockAllMembers = new Mock<IAllMembers>();
            var mockMember = new Mock<IMember>();
            mockMember.Setup(x => x.Name).Returns("Edward");

            mockAllMembers.Setup(x => x.AllMembersList).Returns(new Dictionary<string, IMember>());
            //Act
            allMembersOperation.AddMember(mockAllMembers.Object, mockMember.Object);

            //Assert
            Assert.AreSame(mockAllMembers.Object.AllMembersList["Edward"], mockMember.Object);
        }

        [TestMethod]
        public void Constructor_ShouldReturnCorrectDictionaryOfMembers()
        {
            //Arrange, Act
            var allMembers = new AllMembers();

            //Assert
            Assert.IsInstanceOfType(allMembers.AllMembersList, new Dictionary<string, IMember>().GetType());
        }
    }
}
