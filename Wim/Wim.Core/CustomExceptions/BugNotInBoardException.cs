﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Core.CustomExceptions
{
    public class BugNotInBoardException : ItemNotPresentInAppException
    {
        public BugNotInBoardException()
        {
        }

        public BugNotInBoardException(string message)
            : base(message)
        {
        }

        public BugNotInBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
