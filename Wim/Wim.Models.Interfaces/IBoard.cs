using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IBoard
    {
        string Name { get; }

        List<IWorkItem> WorkItems { get; }

        List<IActivityHistory> ActivityHistory { get; }

        void AddActivityHistoryToBoard(IMember trackedMember, IWorkItem trackedWorkItem);

        void AddActivityHistoryToBoard(IWorkItem trackedWorkItem);

        void AddActivityHistoryToBoard<T>(IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum);

        string ShowBoardActivityToString();

        void AddWorkitemToBoard(IWorkItem workItemToAdd);

        void AddActivityHistoryAfterAssignUnsignToBoard(string workItemTitle, IMember memberToAssign, IMember memberFromUnsign);
    }
}
