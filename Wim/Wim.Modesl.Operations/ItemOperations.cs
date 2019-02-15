using System;
using System.Reflection;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Modesl.Operations
{
    public class ItemOperations : IItemOperations
    {
         public void ChangePriority(IWorkItem itemToChangePriorityFor, string itemType, Priority newPriorityEnum)
         {
            Assembly asm = typeof(Bug).Assembly;
            Type type = Type.GetType($"Wim.Models.{itemType}, {asm}");
            type.GetProperty(newPriorityEnum.GetType().Name).SetValue(type, newPriorityEnum); 

            //itemToChangePriorityFor.Priority = newPriorityEnum;

            //if (itemType == "Bug")
            //{
            //    var castedItem = (IBug)itemToChangePriorityFor;
            //    castedItem.ChangeBugPriority(newPriorityEnum);

            //    castedItem.Priority = newPriorityEnum;
            //}
            //else if (itemType == "Story")
            //{
            //    var castedItem = (IStory)itemToChangePriorityFor;
            //    castedItem.ChangeStoryPriority(newPriorityEnum);
            //}   
        }
    }
}
