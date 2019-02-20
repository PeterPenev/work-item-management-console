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
        private const string ImproperEnumInputted = "The {1} is not valid!";


        private const string PersonExists = "Person with name {0} already exists!";
        private const string PersonCreated = "Person with name {0} was created!";
        private const string NullOrEmptyInput = "{0} cannot be null or empty!";
        
        
        private const string TeamAlreadyExists = "Team with name {0} already exists!";
        
        private const string BugNotInBoard = "There is no bug with name {0} in board {1} part of team {2}!";   

        
        
        
        private const string RatingCannotBeConverted = "The Rating: {0} from the input cannot be converted to Number!";

        private const string NoTeamsInApplication = "There are no Teams in the Application yet!";



        private const string NoPeopleInApplication = "There are no people!";
        private const string MemberDoesNotExist = "The member does not exist!";
        private const string NullOrEmptyTeamName = "Team Name cannot be null or empty!";
        private const string NullOrEmptyMemberName = "Member Name cannot be null or empty!";
        private const string TeamNameExists = "Team Name {0} already exists!";
        private const string TeamCreated = "Team with name {0} was created!";
        private const string TeamDoesNotExist = "Team Name {0} does not exists!";
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";
        private const string NullOrEmptyBoardName = "Board Name cannot be null or empty!!";
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";
        private const string BoardAlreadyExists = "Board with name {0} already exists!";
      
        

        private const string BugCreated = "Bug {0} was created!";

        private const string BoardDoesNotExist = "Board with name {0} doest not exist!";
        private const string NullOrEmptyStoryName = "Story Name cannot be null or empty!";
        private const string StoryCreated = "Story {0} was created!";
        private const string NullOrEmptyFeedbackName = "Feedback Name cannot be null or empty!";
        private const string FeedbackCreated = "Feedback {0} was created!";
        private const string InvalidFeedbackRaiting = "{0} is Invalid Feedback Raiting value!";

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
            if (isEnumConvertableBool)
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

