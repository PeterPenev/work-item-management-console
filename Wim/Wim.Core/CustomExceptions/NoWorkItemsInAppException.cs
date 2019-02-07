using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoWorkItemsInAppException : ItemNotPresentInAppException
    {
        public NoWorkItemsInAppException()
        {
        }

        public NoWorkItemsInAppException(string message)
            : base(message)
        {
        }

        public NoWorkItemsInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
