using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IBoardOperations
    {
        string ShowBoardActivityToString(IBoard board);
        
        void AddWorkitemToBoard(IBoard board, IWorkItem workItemToAdd);

        void AddActivityHistoryToBoard(IBoard board, IMember trackedMember, IWorkItem trackedWorkItem);

        void AddActivityHistoryToBoard(IBoard board, IWorkItem trackedWorkItem);

        void AddActivityHistoryToBoard<T>(IBoard board, IMember trackedMember, IWorkItem trackedWorkItem, T changedEnum);

        void AddActivityHistoryAfterAssignUnsignToBoard(IBoard board, string itemType, string workItemTitle, IMember memberToAssign, IMember memberFromUnsign);
       
    }
}
