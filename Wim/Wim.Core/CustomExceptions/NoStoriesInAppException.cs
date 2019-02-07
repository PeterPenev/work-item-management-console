using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoStoriesInAppException : ItemNotPresentInAppException
    {
        public NoStoriesInAppException()
        {
        }

        public NoStoriesInAppException(string message)
            : base(message)
        {
        }

        public NoStoriesInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
