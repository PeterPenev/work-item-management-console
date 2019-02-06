using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class MemberNotInAppException : ItemNotPresentInAppException
    {
        public MemberNotInAppException()
        {
        }

        public MemberNotInAppException(string message)
            : base(message)
        {
        }

        public MemberNotInAppException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
