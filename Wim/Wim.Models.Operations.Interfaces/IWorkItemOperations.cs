using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IWorkItemOperations
    {
        void AddActivityHistoryToWorkItem(IWorkItem trackedWorkItem, IMember trackedMember);

        void AddActivityHistoryToWorkItem<T>(IWorkItem trackedWorkItem, IMember trackedMember, T changedEnum);

        void AddComment(IWorkItem itemToAddActivityHistoryFor, string commentToAdd, string authorOfComment);
    }
}
