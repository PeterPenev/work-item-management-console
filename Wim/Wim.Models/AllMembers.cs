using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllMembers
    {
        //fields
        private List<IMember> allMembersList;

        //constructor
        public AllMembers()
        {
            this.allMembersList = new List<IMember>();
        }

        //properties
        public List<IMember> AllMembersList
        {
            get
            {
                return new List<IMember>(this.allMembersList);
            }
        }
    }
}
