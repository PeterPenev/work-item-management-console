using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ImproperItemTitleLengthException : Exception
    {
        public ImproperItemTitleLengthException()
        {
        }

        public ImproperItemTitleLengthException(string message)
            : base(message)
        {
        }

        public ImproperItemTitleLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
