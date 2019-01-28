﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wim.Models.Interfaces
{
    public interface IFeedback
    {
        Guid Id { get; set; }
        int Rating { get; set; }
        Status Status { get; }
    }
}