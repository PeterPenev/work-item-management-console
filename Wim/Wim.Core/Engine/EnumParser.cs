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
        private const string InvalidSeverityType = "Invalid Priority type!";
        private const string InvalidStatusType = "Invalid Status type!";
        private const string InvalidStoryStatusType = "Invalid Story Status type!";
        private const string InvalidStorySize = "Invalid Story Size!";
        private const string InvalidFeedbackStatusType = "Invalid Feedback Status type!";
        private const string InvalidFeedbackRaiting = "{0} is Invalid Feedback Raiting value!";

        public EnumParser()
        {
        }

        public Priority GetPriority(string priorityString)
        {
            var isEnumValid = Enum.TryParse(priorityString, out Priority resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidPriorityType);
            }
        }

        public Severity GetSeverity(string severityString)
        {
            var isEnumValid = Enum.TryParse(severityString, out Severity resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidSeverityType);
            }
        }

        public BugStatus GetBugStatus(string bugStatusString)
        {
            var isEnumValid = Enum.TryParse(bugStatusString, out BugStatus resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidStatusType);
            }
        }

        public StoryStatus GetStoryStatus(string storyStatusString)
        {
            var isEnumValid = Enum.TryParse(storyStatusString, out StoryStatus resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidStatusType);
            }
        }

        public Size GetStorySize(string storySizeString)
        {
            var isEnumValid = Enum.TryParse(storySizeString, out Size resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidStorySize);
            }
        }

        public FeedbackStatus GetFeedbackStatus(string feedbackStatusString)
        {
            var isEnumValid = Enum.TryParse(feedbackStatusString, out FeedbackStatus resultedEnum);

            if (isEnumValid)
            {
                return resultedEnum;
            }
            else
            {
                throw new InvalidOperationException(InvalidStatusType);
            }
        }
    }
}
