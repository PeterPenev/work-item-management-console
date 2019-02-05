using System.Collections.Generic;
using System.Text;
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

        //Adding Person to the Global Dictionary of Members
        public void AddMember(IMember member)
        {               
            allMembersList.Add(member.Name, member);                  
        }

        //Returning String Representation of the Members'names in the Dictionary of allMembersInput
        public string ShowAllMembersToString()
        {
            StringBuilder sb = new StringBuilder();

            int numberOfPerson = 1;

            foreach (var item in this.AllMembersList)
            {                
                sb.AppendLine($"{numberOfPerson}. {item.Key}");
                numberOfPerson++;
            }

            return sb.ToString().Trim();
        }
    }
}
