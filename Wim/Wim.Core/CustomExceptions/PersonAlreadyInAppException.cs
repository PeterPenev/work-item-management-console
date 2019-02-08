using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class PersonAlreadyInAppException : ItemAlreadyInApplicationException
    {
        public PersonAlreadyInAppException()
        {
        }

        public PersonAlreadyInAppException(string message)
            : base(message)
        {
        }

        public PersonAlreadyInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
