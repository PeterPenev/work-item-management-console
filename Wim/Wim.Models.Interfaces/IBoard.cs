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

        void AddActivityHistoryToBoard(IBoard boardToAddHistoryTo, IMember trackedMember, IWorkItem trackedWorkItem);

        string ShowBoardActivityToString(IList<IActivityHistory> activityHistoryInput);

        void AddWorkitemToBoard(IWorkItem workItemToAdd);
    }
}
