using System.Collections.Generic;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class AllMembers : IAllMembers
    {
        //fields
        private Dictionary<string, IMember> allMembersList;

        //constructorw
        public AllMembers()
        {
            this.allMembersList = new Dictionary<string, IMember>();
        }

        //properties
        public IDictionary<string, IMember> AllMembersList
        {
            get
            {
                return new Dictionary<string, IMember>(this.allMembersList);
            }
        }

        //methods
        public void AddMember(IMember member)
        {               
            allMembersList.Add(member.Name, member);                  
        }
    }
}
