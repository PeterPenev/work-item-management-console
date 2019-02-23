using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Board : IBoard
    {
        //Constructors
        public Board(string name)
        {
            this.Name = name;
            this.WorkItems = new List<IWorkItem>();
            this.ActivityHistory = new List<IActivityHistory>();
        }

        //Properties
        public string Name { get; set; }

        public List<IWorkItem> WorkItems { get; }

        public List<IActivityHistory> ActivityHistory { get; }        
    }
}
