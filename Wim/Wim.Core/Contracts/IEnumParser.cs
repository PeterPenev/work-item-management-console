using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;

namespace Wim.Core.Contracts
{
    public interface IEnumParser
    {
        Priority GetPriority(string priorityString);

        Severity GetSeverity(string severityString);

        BugStatus GetBugStatus(string bugStatusString);

        StoryStatus GetStoryStatus(string storyStatusString);

        Size GetStorySize(string storySizeString);

        FeedbackStatus GetFeedbackStatus(string feedbackStatusString);
    }
}
