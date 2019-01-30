using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Feedback : IFeedback
    {
        public Guid Id { get; private set; }

        private int rating;

        public Feedback(int rating, Status status)
        {
            this.rating = rating;
            this.Status = status;
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

        public Status Status { get; }
    }
}
