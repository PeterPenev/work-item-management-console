using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class StoryAlreadyInBoardException : ItemAlreadyInApplicationException
    {
        public StoryAlreadyInBoardException()
        {
        }

        public StoryAlreadyInBoardException(string message)
            : base(message)
        {
        }

        public StoryAlreadyInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
