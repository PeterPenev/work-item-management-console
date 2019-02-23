using System;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class ActivityHistory : IActivityHistory
    {
        //Constructors
        public ActivityHistory(string message)
        {
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.LoggingDate = DateTime.Now;
        }

        //Properties
        public Guid Id { get; }      

        public string Message { get; }

        public DateTime LoggingDate { get; }
    }
}
