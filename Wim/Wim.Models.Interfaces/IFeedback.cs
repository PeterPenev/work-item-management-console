using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Models.Interfaces
{
    public interface IFeedback
    {
        Guid Id { get; }

        int Rating { get; set; }

        FeedbackStatus FeedbackStatus { get; }
    }
}
