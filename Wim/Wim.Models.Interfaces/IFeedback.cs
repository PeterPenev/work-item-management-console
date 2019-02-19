using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IFeedback : IWorkItem
    {     
        int Rating { get; set; }

        FeedbackStatus FeedbackStatus { get; set; }

        void ChangeFeedbackRating(int rating);

        void ChangeFeedbackStatus(FeedbackStatus status);
    }
}
