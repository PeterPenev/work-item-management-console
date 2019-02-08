using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class BoardAlreadyExistsInTeamException : ItemAlreadyInApplicationException
    {
        public BoardAlreadyExistsInTeamException()
        {
        }

        public BoardAlreadyExistsInTeamException(string message)
            : base(message)
        {
        }

        public BoardAlreadyExistsInTeamException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
