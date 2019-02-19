using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IWorkItemOperations
    {
        void AddActivityHistoryToWorkItem(IWorkItem itemToAddActivityHistoryFor, IMember trackedMember, IWorkItem trackedWorkItem);

        void AddActivityHistoryToWorkItem<T>(IWorkItem itemToAddActivityHistoryFor, IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum);

        void AddComment(IWorkItem itemToAddActivityHistoryFor, string commentToAdd, string authorOfComment);
    }
}
