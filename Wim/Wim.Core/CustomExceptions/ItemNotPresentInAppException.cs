using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class ItemNotPresentInAppException : Exception
    {
        public ItemNotPresentInAppException()
        {
        }

        public ItemNotPresentInAppException(string message)
            : base(message)
        {
        }

        public ItemNotPresentInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
