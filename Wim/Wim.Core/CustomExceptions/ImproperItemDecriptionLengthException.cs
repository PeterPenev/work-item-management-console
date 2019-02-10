using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ImproperItemDecriptionLengthException : Exception
    {
        public ImproperItemDecriptionLengthException()
        {
        }

        public ImproperItemDecriptionLengthException(string message)
            : base(message)
        {
        }

        public ImproperItemDecriptionLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
