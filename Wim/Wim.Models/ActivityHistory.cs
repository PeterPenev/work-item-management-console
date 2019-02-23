using System;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class ActivityHistory : IActivityHistory
    {
        //Fields
        private string message;
        private DateTime loggingDate;

        //Constructors
        public ActivityHistory(string message)
        {
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.LoggingDate = DateTime.Now;
        }

        //Properties
        public Guid Id { get; private set; }      

        public string Message
        {
            get
            {
                return this.message;
            }
            private set
            {
                this.message = value;
            }
        }

        public DateTime LoggingDate
        {
            get
            {
                return this.loggingDate;
            }
            private set
            {
                this.loggingDate = value;
            }
        }
    }
}
