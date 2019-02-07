using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class PersonAlreadyInBoardException : ItemAlreadyInApplicationException
    {
        public PersonAlreadyInBoardException()
        {
        }

        public PersonAlreadyInBoardException(string message)
            : base(message)
        {
        }

        public PersonAlreadyInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
