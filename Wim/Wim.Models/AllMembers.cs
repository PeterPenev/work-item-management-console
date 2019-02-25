using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllMembers : IAllMembers
    {
        //Constructor
        public AllMembers()
        {
            this.AllMembersList = new Dictionary<string, IMember>();
        }

        //Properties
        public IDictionary<string, IMember> AllMembersList { get; }             
    }
}
