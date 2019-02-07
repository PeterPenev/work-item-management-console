using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class TeamAlreadyInBoardException : ItemAlreadyInApplicationException
    {
        public TeamAlreadyInBoardException()
        {
        }

        public TeamAlreadyInBoardException(string message)
            : base(message)
        {
        }

        public TeamAlreadyInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
