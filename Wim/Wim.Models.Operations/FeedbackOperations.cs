using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;
using Wim.Models.Operations.Interfaces;

namespace Wim.Models.Operations
{
    public class FeedbackOperations : WorkItemOperations, IFeedbackOperations
    {
        public void ChangeFeedbackRating(IWorkItem workItem, int rating )
        {
            var feedback = workItem as Feedback;
            feedback.Rating = rating;
        }

        public void ChangeFeedbackStatus(IWorkItem workItem, FeedbackStatus status)
        {
            var feedback = workItem as Feedback;
            feedback.FeedbackStatus = status;
        }

    }
}
