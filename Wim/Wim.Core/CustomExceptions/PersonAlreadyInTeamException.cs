using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class PersonAlreadyInTeamException : Exception
    {
        public PersonAlreadyInTeamException()
        {
        }

        public PersonAlreadyInTeamException(string message)
            : base(message)
        {
        }

        public PersonAlreadyInTeamException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
