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

        void AddActivityHistoryAfterAssignToMember(IMember member, string itemType, string workItemTitle, IMember memberToAssign);

        void AddActivityHistoryAfterUnsignToMember(IMember member, string itemType, string workItemTitle, IMember memberFromUnsign);

        void RemoveWorkItemIdToMember(IMember member, Guid workItemIdInput);

        string ShowMemberActivityToString(IMember member);
    }
}
