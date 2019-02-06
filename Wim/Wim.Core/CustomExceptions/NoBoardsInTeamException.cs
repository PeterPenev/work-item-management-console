using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoBoardsInTeamException : ItemNotPresentInAppException
    {
        public NoBoardsInTeamException()
        {
        }

        public NoBoardsInTeamException(string message)
            : base(message)
        {
        }

        public NoBoardsInTeamException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
