using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoSuchFeedbackInBoardException : Exception
    {
        public NoSuchFeedbackInBoardException()
        {
        }

        public NoSuchFeedbackInBoardException(string message)
            : base(message)
        {
        }

        public NoSuchFeedbackInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
