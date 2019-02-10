using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ImproperBoardNameLengthException : Exception
    {
        public ImproperBoardNameLengthException()
        {
        }

        public ImproperBoardNameLengthException(string message)
            : base(message)
        {
        }

        public ImproperBoardNameLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
