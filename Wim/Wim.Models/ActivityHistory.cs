using System;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class ActivityHistory : IActivityHistory
    {
        //field
        private string message;
        private DateTime loggingDate;


        //constructor
        public ActivityHistory(string message)
        {
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.LoggingDate = DateTime.Now;
        }

        public Guid Id { get; private set; }      

        public string Message
        {
            get
            {
                return this.message;
            }
            set
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
            set
            {
                this.loggingDate = value;
            }
        }

        //methods
    }
}
