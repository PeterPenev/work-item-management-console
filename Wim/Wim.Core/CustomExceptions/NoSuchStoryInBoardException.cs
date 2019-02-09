using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoSuchStoryInBoardException : Exception
    {
        public NoSuchStoryInBoardException()
        {
        }

        public NoSuchStoryInBoardException(string message)
            : base(message)
        {
        }

        public NoSuchStoryInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
