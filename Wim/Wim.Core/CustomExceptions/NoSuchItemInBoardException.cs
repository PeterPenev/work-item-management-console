using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoSuchItemInBoardException : Exception
    {
        public NoSuchItemInBoardException()
        {
        }

        public NoSuchItemInBoardException(string message)
            : base(message)
        {
        }

        public NoSuchItemInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
