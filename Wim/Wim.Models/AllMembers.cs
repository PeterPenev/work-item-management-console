using System.Collections.Generic;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllMembers : IAllMembers
    {
        //Fields
        private Dictionary<string, IMember> allMembersList;

        //Constructor
        public AllMembers()
        {
            this.allMembersList = new Dictionary<string, IMember>();
        }

        //Properties
        public IDictionary<string, IMember> AllMembersList
        {
            get
            {
                return this.allMembersList;
            }
        }     
    }
}
