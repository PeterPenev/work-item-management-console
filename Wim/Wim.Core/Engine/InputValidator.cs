using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.CustomExceptions;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public class InputValidator : IInputValidator
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string ImproperMemberNameLength = "Member name should be between 5 and 15 symbols!";
        private const string ImproperBoardNameLength = "Board name should be between 5 and 10 symbols!";
        private const string ImproperItemTitleLength = "Item title should be between 10 and 50 symbols!";
        private const string ImproperItemDescriptionLength = "Item description should be between 10 and 500 symbols!";
        private const string ImproperEnumInputted = "The {0} is not valid!";
        private const string NoTeamsInApplication = "There are no Teams in the Application yet!";  
        private const string RatingCannotBeConverted = "The Rating: {0} from the input cannot be converted to Number!";

        public InputValidator()
        {
        }

        public void IsNullOrEmpty(string inputToCheck, string inputType)
        {
            if (string.IsNullOrEmpty(inputToCheck))
            {
                throw new ArgumentNullException(NoTeamsInApplication);
            }
        }

        public void IsEnumConvertable<T>(bool isEnumConvertableBool, T enumTypeForConverting)
        {
            if (!isEnumConvertableBool)
            {
                var ImproperEnumInputtedMessage = string.Format(ImproperEnumInputted, enumTypeForConverting.GetType().Name);
                throw new ArgumentException(ImproperEnumInputtedMessage);
            }
        }

        public void ValdateMemberNameLength(string memberNameToCheck)
        {
            if (memberNameToCheck.Length < 5 || memberNameToCheck.Length > 10)
            {
                throw new ImproperMemberNameLengthException(ImproperMemberNameLength);
            }
        }

        public void ValdateBoardNameLength(string boardNameToCheck)
        {
            if (boardNameToCheck.Length < 5 || boardNameToCheck.Length > 10)
            {
                throw new ImproperBoardNameLengthException(ImproperBoardNameLength);
            }
        }

        public void ValdateItemTitleLength(string itemTitleToCheck)
        {
            if (itemTitleToCheck.Length < 10 || itemTitleToCheck.Length > 50)
            {
                throw new ImproperItemDecriptionLengthException(ImproperItemDescriptionLength);
            }
        }

        public void ValdateItemDescriptionLength(string itemTitleToCheck)
        {
            if (itemTitleToCheck.Length < 10 || itemTitleToCheck.Length > 500)
            {
                throw new ImproperItemTitleLengthException(ImproperItemTitleLength);
            }
        }

        public int ValidateRatingConversion(string ratingForCheck)
        {
            bool isRatingConvertable = int.TryParse(ratingForCheck, out var intFeedbackRaiting);
            if (!isRatingConvertable)
            {
                var RatingCannotBeConvertedMessage = string.Format(RatingCannotBeConverted, ratingForCheck);
                throw new RatingCannotBeConvertedException(RatingCannotBeConvertedMessage);
            }
            return intFeedbackRaiting;
        }
    }
 
}

