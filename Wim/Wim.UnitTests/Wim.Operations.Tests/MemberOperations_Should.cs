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

        [TestMethod]
        public void AddActivityHistoryToMember_ShouldAddCorrectActivityHistory()
        {
            //Arrange
            var memberOperations = new MemberOperations();
            var mockMember = new Mock<IMember>();
            mockMember.Setup(x => x.Name).Returns("Gosho");
            mockMember.Setup(x => x.ActivityHistory).Returns(new List<IActivityHistory>());
            var mockTrackedWorkItem = new Mock<IBug>();
            mockTrackedWorkItem.Setup(x => x.Title).Returns("bug1234567");
            var mockTrackedTeam = new Mock<ITeam>();
            mockTrackedTeam.Setup(x => x.Name).Returns("Alpha");
            var mockTrackedBoard = new Mock<IBoard>();
            mockTrackedBoard.Setup(x => x.Name).Returns("TestBoard");

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Member: Gosho created: IBugProxy with Title: bug1234567 in Board: TestBoard part of Alpha Team!");

            string resultToAddAssMessage = strBuilder.ToString().Trim();
            var activityHistoryToAddToMember = mockMember.Object.ActivityHistory;

            //Act
            memberOperations.AddActivityHistoryToMember(mockMember.Object, mockTrackedWorkItem.Object, mockTrackedTeam.Object, mockTrackedBoard.Object);

            //Assert
            Assert.AreEqual(mockMember.Object.ActivityHistory[0].Message, resultToAddAssMessage);
        }

        [TestMethod]
        public void RemoveWorkItemIdToMember_ShouldAssignCorrectId()
        {
            //Arrange
            var guid = new Guid();
            var mockMember = new Mock<IMember>();
            mockMember.Setup(x => x.WorkItemsId).Returns(new List<Guid>());
            var mockTrackedWorkItem = new Mock<IBug>();
            mockTrackedWorkItem.Setup(x => x.Id).Returns(guid);

            var memberOperations = new MemberOperations();

            //Act
            memberOperations.RemoveWorkItemIdToMember(mockMember.Object, guid);

            //Assert
            Assert.AreEqual(mockMember.Object.WorkItemsId.Count,0);
        }
    }
}
