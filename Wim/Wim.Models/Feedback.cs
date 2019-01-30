using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Feedback : IFeedback
    {
        public Guid Id { get; private set; }

        private int rating;

        public Feedback(int rating, FeedbackStatus feedbackStatus)
        {
            this.rating = rating;
            this.FeedbackStatus = feedbackStatus;
        }

        public int Rating
        {
            get
            {
                return this.rating;
            }
            set
            {
                this.rating = value;
            }
        }

        public FeedbackStatus FeedbackStatus { get; }
    }
}
