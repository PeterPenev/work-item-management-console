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
        public void ChangeFeedbackRating(IFeedback feedback, int rating )
        {
            feedback.Rating = rating;
        }

        public void ChangeFeedbackStatus(IFeedback feedback, FeedbackStatus status)
        {
            feedback.FeedbackStatus = status;
        }

    }
}
