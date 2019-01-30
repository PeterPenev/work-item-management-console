using System;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    class ActivityHistory : IActivityHistory
    {
        //field
        private Guid id;
        private string message;
        private DateTime logDate;

        //constructor
        public ActivityHistory(string message)
            {
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.LogDate = DateTime.Now;
            }

        //properties
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

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

        public DateTime LogDate
        {
            get
            {
                return this.logDate;
            }
            set
            {
                this.logDate = value;
            }
        }

        //methods
    }
}
