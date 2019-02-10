using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ImproperMemberNameLengthException : Exception
    {
        public ImproperMemberNameLengthException()
        {
        }

        public ImproperMemberNameLengthException(string message)
            : base(message)
        {
        }

        public ImproperMemberNameLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
