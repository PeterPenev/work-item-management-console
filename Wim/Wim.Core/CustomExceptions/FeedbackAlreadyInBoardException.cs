using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class FeedbackAlreadyInBoardException : ItemAlreadyInApplicationException
    {
        public FeedbackAlreadyInBoardException()
        {
        }

        public FeedbackAlreadyInBoardException(string message)
            : base(message)
        {
        }

        public FeedbackAlreadyInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
