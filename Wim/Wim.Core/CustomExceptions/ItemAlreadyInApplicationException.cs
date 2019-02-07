using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ItemAlreadyInApplicationException : Exception
    {
        public ItemAlreadyInApplicationException()
        {
        }

        public ItemAlreadyInApplicationException(string message)
            : base(message)
        {
        }

        public ItemAlreadyInApplicationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
