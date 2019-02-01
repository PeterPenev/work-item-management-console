﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Feedback : WorkItem, IFeedback
    {
        private int rating;

        public Feedback(string title, string description, int rating, FeedbackStatus feedbackStatus)
            :base(title, description)
        {
            this.Rating = rating;
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
