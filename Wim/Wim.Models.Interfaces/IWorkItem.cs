using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IWorkItem
    {
        Guid Id { get;  }

        string Title { get; }

        string Description { get; }

        List<string> Comments { get; }

        List<IActivityHistory> ActivityHistory { get; }

        void AddActivityHistoryToWorkItem(IMember trackedMember, IWorkItem trackedWorkItem);

        void AddActivityHistoryToWorkItem<T>(IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum);

        void AddComment(string commentToAdd, string authorOfComment);
    }
}
