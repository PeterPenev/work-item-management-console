using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class BoardNotInAppException : ItemNotPresentInAppException
    {
        public BoardNotInAppException()
        {
        }

        public BoardNotInAppException(string message)
            : base(message)
        {
        }

        public BoardNotInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
