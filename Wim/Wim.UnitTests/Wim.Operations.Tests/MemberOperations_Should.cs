using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Operations;
using Wim.Models.Operations.Interfaces;

namespace Wim.UnitTests.Wim.Operations.Tests
{
    [TestClass]
    public class MemberOperations_Should
    {
        [TestMethod]
        public void AddWorkItemIdToMember_ShouldAssignCorrectId()
        {
            //Arrange
            var guid = new Guid();
            var mockMember = new Mock<IMember>();
            mockMember.Setup(x => x.WorkItemsId).Returns(new List<Guid>());
            var memberOperations = new MemberOperations();

            //Act
            memberOperations.AddWorkItemIdToMember(mockMember.Object, guid);

            //Assert
            Assert.AreEqual(guid, mockMember.Object.WorkItemsId[0]);
        }

    }
}
