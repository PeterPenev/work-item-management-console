using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoTeamsInAppException : ItemNotPresentInAppException
    {
        public NoTeamsInAppException()
        {
        }

        public NoTeamsInAppException(string message)
            : base(message)
        {
        }

        public NoTeamsInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
