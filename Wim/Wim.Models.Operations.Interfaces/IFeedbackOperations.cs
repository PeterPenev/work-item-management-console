using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models.Operations.Interfaces
{
    public interface IFeedbackOperations : IWorkItemOperations
    {
        void ChangeFeedbackRating(IWorkItem workItem, int rating);

        void ChangeFeedbackStatus(IWorkItem workItem, FeedbackStatus status);
    }
}
