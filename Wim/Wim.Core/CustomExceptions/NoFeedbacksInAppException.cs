using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoFeedbacksInAppException : ItemNotPresentInAppException
    {
        public NoFeedbacksInAppException()
        {
        }

        public NoFeedbacksInAppException(string message)
            : base(message)
        {
        }

        public NoFeedbacksInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
