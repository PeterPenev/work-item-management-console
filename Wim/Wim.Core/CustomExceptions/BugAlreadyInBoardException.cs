using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class BugAlreadyInBoardException : ItemAlreadyInApplicationException
    {
        public BugAlreadyInBoardException()
        {
        }

        public BugAlreadyInBoardException(string message)
            : base(message)
        {
        }

        public BugAlreadyInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
