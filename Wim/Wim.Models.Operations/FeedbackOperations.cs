using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;
using Wim.Models.Enums;

namespace Wim.Models.Operations
{
    public class FeedbackOperations
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
