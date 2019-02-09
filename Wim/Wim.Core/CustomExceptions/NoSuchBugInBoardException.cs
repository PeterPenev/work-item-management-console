using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoSuchBugInBoardException : Exception
    {
        public NoSuchBugInBoardException()
        {
        }

        public NoSuchBugInBoardException(string message)
            : base(message)
        {
        }

        public NoSuchBugInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
