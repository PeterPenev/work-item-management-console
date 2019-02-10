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
                return new Dictionary<string, IMember>(this.allMembersList);
            }
        }

        //Methods
        public void AddMember(IMember member)
        {               
            allMembersList.Add(member.Name, member);                  
        }

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
