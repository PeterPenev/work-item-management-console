using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoBugsInAppException : ItemNotPresentInAppException
    {
        public NoBugsInAppException()
        {
        }

        public NoBugsInAppException(string message)
            : base(message)
        {
        }

        public NoBugsInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
