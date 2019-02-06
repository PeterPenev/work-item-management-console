using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;

namespace Wim.Core.Engine
{
    public class EnumParser : IEnumParser
    {
        private const string InvalidPriorityType = "Invalid Priority type!";
        private const string InvalidStatusType = "Invalid Status type!";
        private const string InvalidStoryStatusType = "Invalid Story Status type!";
        private const string InvalidStorySizeType = "Invalid Story Size type!";
        private const string InvalidFeedbackStatusType = "Invalid Feedback Status type!";
        private const string InvalidFeedbackRaiting = "{0} is Invalid Feedback Raiting value!";

        public EnumParser()
        {
        }

        public Priority GetPriority(string priorityString)
        {
            switch (priorityString.ToLower())
            {
                case "high":
                    return Priority.High;
                case "medium":
                    return Priority.Medium;
                case "low":
                    return Priority.Low;
                default:
                    throw new InvalidOperationException(InvalidPriorityType);
            }
        }

        public Severity GetSeverity(string severityString)
        {
            switch (severityString.ToLower())
            {
                case "critical":
                    return Severity.Critical;
                case "major":
                    return Severity.Major;
                case "minor":
                    return Severity.Minor;
                default:
                    throw new InvalidOperationException(InvalidPriorityType);
            }
        }

        public BugStatus GetBugStatus(string bugStatusString)
        {
            switch (bugStatusString.ToLower())
            {
                case "active":
                    return BugStatus.Active;
                case "fixed":
                    return BugStatus.Fixed;
                default:
                    throw new InvalidOperationException(InvalidStatusType);
            }
        }

        public StoryStatus GetStoryStatus(string storyStatusString)
        {
            switch (storyStatusString.ToLower())
            {
                case "notdone":
                    return StoryStatus.NotDone;
                case "inprogress":
                    return StoryStatus.InProgress;
                case "done":
                    return StoryStatus.Done;
                default:
                    throw new InvalidOperationException(InvalidStoryStatusType);
            }
        }

        public Size GetStorySize(string storySizeString)
        {
            switch (storySizeString.ToLower())
            {
                case "small":
                    return Size.Small;
                case "medium":
                    return Size.Medium;
                case "large":
                    return Size.Large;
                default:
                    throw new InvalidOperationException(InvalidStorySizeType);
            }
        }

        public FeedbackStatus GetFeedbackStatus(string feedbackStatusString)
        {
            switch (feedbackStatusString.ToLower())
            {
                case "new":
                    return FeedbackStatus.New;
                case "unscheduled":
                    return FeedbackStatus.Unscheduled;
                case "scheduled":
                    return FeedbackStatus.Scheduled;
                case "done":
                    return FeedbackStatus.Done;
                default:
                    throw new InvalidOperationException(InvalidFeedbackStatusType);
            }
        }
    }
}
