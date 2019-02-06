using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoMembersInAppException : ItemNotPresentInAppException
    {
        public NoMembersInAppException()
        {
        }

        public NoMembersInAppException(string message)
            : base(message)
        {
        }

        public NoMembersInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
