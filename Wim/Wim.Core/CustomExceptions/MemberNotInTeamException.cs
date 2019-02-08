using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class MemberNotInTeamException : Exception
    {
        public MemberNotInTeamException()
        {
        }

        public MemberNotInTeamException(string message)
            : base(message)
        {
        }

        public MemberNotInTeamException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
