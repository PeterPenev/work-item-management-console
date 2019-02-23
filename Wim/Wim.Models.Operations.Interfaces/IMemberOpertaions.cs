using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IMemberOpertaions
    {
        void AddWorkItemIdToMember(IMember member, Guid workItemIdInput);

        void AddActivityHistoryToMember(IMember member, IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard);

        void AddActivityHistoryToMember<T>(IMember member, IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard, T changedEnum);

        void AddActivityHistoryAfterAssignToMember(IMember memberToAssign, string itemType, string workItemTitle);

        void AddActivityHistoryAfterUnsignToMember(IMember memberFromUnsign, string itemType, string workItemTitle);

        void RemoveWorkItemIdToMember(IMember member, Guid workItemIdInput);

        string ShowMemberActivityToString(IMember member);
    }
}
