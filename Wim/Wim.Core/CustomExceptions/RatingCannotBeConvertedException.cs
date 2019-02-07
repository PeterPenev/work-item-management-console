using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class RatingCannotBeConvertedException : Exception
    {
        public RatingCannotBeConvertedException()
        {
        }

        public RatingCannotBeConvertedException(string message)
            : base(message)
        {
        }

        public RatingCannotBeConvertedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
