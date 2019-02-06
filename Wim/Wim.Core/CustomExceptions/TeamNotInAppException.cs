using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class TeamNotInAppException : ItemNotPresentInAppException
    {
        public TeamNotInAppException()
        {
        }

        public TeamNotInAppException(string message)
            : base(message)
        {
        }

        public TeamNotInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
