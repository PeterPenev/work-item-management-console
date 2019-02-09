using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IMember
    {
        string Name { get; }

        List<Guid> WorkItemsId { get; }

        List<IActivityHistory> ActivityHistory { get; }

        void AddWorkItemIdToMember(Guid workItemIdInput);

        void AddActivityHistoryToMember(IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard);

        void AddActivityHistoryToMember<T>(IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard, T changedEnum);

        string ShowMemberActivityToString();

        bool FindIfMemberIsAssigned(IDictionary<string, ITeam> allTeamsInput);

        void AddActivityHistoryAfterAssignToMember(string itemType, string workItemTitle, IMember memberToAssign);

        void AddActivityHistoryAfterUnsignToMember(string itemType, string workItemTitle, IMember memberFromUnsign);

        void RemoveWorkItemIdToMember(Guid workItemIdInput);
    }
}
