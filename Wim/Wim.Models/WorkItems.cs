using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public abstract class WorkItems : IWorkItems
    {
        private string title;
        private string description;
        private List<string> comments;
        private List<string> history;

        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                if(value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Title should be between 10 and 50 symbols");
                }
                this.title = value;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
            private set
            {
                if(value.Length < 10 || value.Length > 500)
                {
                    throw new ArgumentOutOfRangeException("Description should be between 10 and 500 symbols");
                }
            }
        }
        public List<string> Comments
        {
            get
            {
                return new List<string>(this.comments);
            }
        }
        public List<string> History
        {
            get
            {
                return new List<string>(this.history);
            }
        }
    }
}
