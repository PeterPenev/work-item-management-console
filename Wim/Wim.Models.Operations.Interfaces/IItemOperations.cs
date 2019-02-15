using System;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IItemOperations
    {
        void ChangePriority(IWorkItem itemToChangePriorityFor, string itemType, Priority newPriorityEnum);
    }
}
