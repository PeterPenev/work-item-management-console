using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class NoSuchBoardInTeamException : ItemNotPresentInAppException
    {
        public NoSuchBoardInTeamException()
        {
        }

        public NoSuchBoardInTeamException(string message)
            : base(message)
        {
        }

        public NoSuchBoardInTeamException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
