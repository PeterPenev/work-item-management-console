using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Feedback : WorkItem, IFeedback
    {
        //Constructors
        public Feedback(string title, string description, int rating, FeedbackStatus feedbackStatus)
            :base(title, description)
        {
            this.Rating = rating;
            this.FeedbackStatus = feedbackStatus;
        }

        //Properties
        public int Rating { get; set; }

        public FeedbackStatus FeedbackStatus { get; set; }       
    }
}
