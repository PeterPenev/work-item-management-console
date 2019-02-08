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
        private const string NoTeamsInApplication = "There are no Teams in the Application yet!";

        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string PersonExists = "Person with name {0} already exists!";
        private const string PersonCreated = "Person with name {0} was created!";
        private const string NullOrEmptyInput = "{0} cannot be null or empty!";
        private const string NoSuchMemberInApplication = "There is no member with {0} name in the Application!";
        private const string NoSuchTeamInApplication = "There is no team with {0} name in the Application!";
        private const string NoMembersInApplication = "There are no Members in the Application yet!";
        private const string PersonAlreadyExists = "Person with name {0} already exists!";
        private const string TeamAlreadyExists = "Team with name {0} already exists!";
        private const string NoSuchBoardInTeam = "There is no board with name {0} in team {1}!";
        private const string BugNotInBoard = "There is no bug with name {0} in board {1} part of team {2}!";
        private const string BoardAlreadyExistsInTeam = "Board with name {0} already exists in team {1}!";

        private const string NoWorkItemsInApp = "There are no work items in the whole app yet!";
        private const string NoBugsInApp = "There are no Bugs in the whole app yet!";
        private const string NoStoriesInApp = "There are no Stories in the whole app yet!";
        private const string NoFeedbacksInApp = "There are no Feedbacks in the whole app yet!";
        private const string PersonAlreadyInTeam = "Person with name {0} is already in team {1}!";

        private const string BugAlreadyExists = "Bug with name {0} in Board: {1} part of Team {2} already exists!";
        private const string StoryAlreadyExists = "Story with name {0} in Board: {1} part of Team {2} already exists!";
        private const string FeedbackAlreadyExists = "Feedback with name {0} in Board: {1} part of Team {2} already exists!";
        private const string RatingCannotBeConverted = "The Rating: {0} from the input cannot be converted to Number!";


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
        private const string NoBoardsInTeam = "There are no boards in this team!";
        private const string NoSuchFeedbackInBoard = "There is no Feedback with name: {0} in board: {1} part of team: {2}!";

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

        public void ValdateIfAnyTeamsExist(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Count == 0)
            {
                throw new NoTeamsInAppException(NoTeamsInApplication);
            }
        }

        public void ValdateIfAnyMembersExist(IAllMembers allMembers)
        {
            if (allMembers.AllMembersList.Count == 0)
            {
                throw new NoMembersInAppException(NoMembersInApplication);
            }
        }

        public void ValdateIfBoardsExistInTeam(IAllTeams allTeams, string teamToShowBoardsFor)
        {
            if (allTeams.AllTeamsList[teamToShowBoardsFor].Boards.Count() == 0)
            {
                throw new NoBoardsInTeamException(NoBoardsInTeam);
            }
        }

        public void ValidateBoardExistanceInTeam(IAllTeams allTeams, string boardNameToCheckFor, string teamToCheckForBoard)
        {
            if (allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).Where(board => board.Name == boardNameToCheckFor).ToList().Count == 0)
            {
                var NoSuchBoardInTeamMessage = string.Format(NoSuchBoardInTeam, boardNameToCheckFor, teamToCheckForBoard);
                throw new NoSuchBoardInTeamException(NoSuchBoardInTeamMessage);
            }
        }

        public void ValidateBoardAlreadyInTeam(IAllTeams allTeams, string boardToAddToTeam, string teamForAddingBoardTo)
        {
            var boardMatches = allTeams.AllTeamsList[teamForAddingBoardTo].Boards
             .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddToTeam);

            if (boardMatches.Count() > 0)
            {
                var BoardAlreadyExistsInTeamMessage = string.Format(BoardAlreadyExistsInTeam, boardToAddToTeam, teamForAddingBoardTo);
                throw new BoardAlreadyExistsInTeamException(BoardAlreadyExistsInTeamMessage);
            }
        }

        public void ValidateMemberExistance(IAllMembers allMembers, string memberName)
        {
            if (!allMembers.AllMembersList.ContainsKey(memberName))
            {
                var NoSuchMemberInApplicationMessage = string.Format(NoSuchMemberInApplication, memberName);
                throw new MemberNotInAppException(NoSuchMemberInApplicationMessage);
            }
        }

        public void ValidateTeamExistance(IAllTeams allTeams, string teamName)
        {            
            if (!allTeams.AllTeamsList.ContainsKey(teamName))
            {
                var NoSuchTeamInApplicationMessage = string.Format(NoSuchTeamInApplication, teamName);
                throw new TeamNotInAppException(NoSuchTeamInApplicationMessage);
            }
        }
        
        public void ValidateBugExistanceInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle)
        {
            var boardToCheckBugFor = allTeams.AllTeamsList[teamToAddBugFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor).First();

            var doesBugExistInBoard = boardToCheckBugFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Bug)).Any(bugThatExists => bugThatExists.Title == bugTitle);

            if (doesBugExistInBoard)
            {
                var BugAlreadyExistsMessage = string.Format(BugAlreadyExists, bugTitle, boardToAddBugFor, teamToAddBugFor);
                throw new BugAlreadyInBoardException(BugAlreadyExistsMessage);
            }
        }

        public void ValidateBugNotInBoard(IAllTeams allTeams, string boardToAddBugFor, string teamToAddBugFor, string bugTitle)
        {
            var boardToCheckBugFor = allTeams.AllTeamsList[teamToAddBugFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor).First();

            var doesBugExistInBoard = boardToCheckBugFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Bug)).Any(bugThatExists => bugThatExists.Title == bugTitle);

            if (!doesBugExistInBoard)
            {
                var BugNotInBoardMessage = string.Format(BugNotInBoard, bugTitle, boardToAddBugFor, teamToAddBugFor);
                throw new BugNotInBoardException(BugNotInBoardMessage);
            }
        }

        public void ValidateStoryExistanceInBoard(IAllTeams allTeams, string boardToAddStoryFor, string teamToAddStoryFor, string storyTitle)
        {
            var boardToCheckStoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddStoryFor).First();

            var doesStoryExistInBoard = boardToCheckStoryFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Story)).Any(storyThatExists => storyThatExists.Title == storyTitle);

            if (doesStoryExistInBoard)
            {
                var StoryAlreadyExistsMessage = string.Format(StoryAlreadyExists, storyTitle, boardToAddStoryFor, teamToAddStoryFor);
                throw new StoryAlreadyInBoardException(StoryAlreadyExistsMessage);
            }
        }

        public void ValidateFeedbackExistanceInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle)
        {
            var boardToCheckFeedbackFor = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddFeedbackFor).First();

            var doesFeedbackExistInBoard = boardToCheckFeedbackFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Feedback))
                   .Any(feedbackThatExists => feedbackThatExists.Title == feedbackTitle);

            if (doesFeedbackExistInBoard)
            {
                var FeedbackAlreadyExistsMessage = string.Format(FeedbackAlreadyExists, feedbackTitle, boardToAddFeedbackFor, teamToAddFeedbackFor);
                throw new FeedbackAlreadyInBoardException(FeedbackAlreadyExistsMessage);
            }
        }

        public void ValidateNoSuchFeedbackInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle)
        {
            var boardToCheckFeedbackFor = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddFeedbackFor).First();

            var doesFeedbackExistInBoard = boardToCheckFeedbackFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Feedback))
                   .Any(feedbackThatExists => feedbackThatExists.Title == feedbackTitle);

            if (!doesFeedbackExistInBoard)
            {
                var NoSuchFeedbackInBoardExceptionMessage = string.Format(NoSuchFeedbackInBoard, feedbackTitle, boardToAddFeedbackFor, teamToAddFeedbackFor);
                throw new NoSuchFeedbackInBoardException(NoSuchFeedbackInBoardExceptionMessage);
            }
        }

        public void ValidateIfPersonExists(IAllMembers allMembers, string personName)
        {

            if (allMembers.AllMembersList.ContainsKey(personName))
            {
                var PersonAlreadyExistsMessage = string.Format(PersonAlreadyExists, personName);
                throw new PersonAlreadyInAppException(PersonAlreadyExistsMessage);
            }
        }

        public void ValidateIfMemberAlreadyInTeam(IAllTeams allTeams, string teamToCheckForPerson, string personName)
        {

            if (allTeams.AllTeamsList[teamToCheckForPerson]
                .Members.Where(member => member.Name == personName)
                 .ToList().Count > 0)
            {
                var PersonAlreadyInTeamMessage = string.Format(PersonAlreadyInTeam, personName, teamToCheckForPerson);
                throw new PersonAlreadyInTeamException(PersonAlreadyInTeamMessage);
            }
        }

        public void ValidateIfTeamExists(IAllTeams allTeams, string teamName)
        {

            if (allTeams.AllTeamsList.ContainsKey(teamName))
            {
                var TeamAlreadyExistsMessage = string.Format(TeamAlreadyExists, teamName);
                throw new TeamAlreadyInBoardException(TeamAlreadyExistsMessage);
            }
        }

        public void ValidateIfAnyWorkItemsExist(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).ToList().Count() == 0)
            {                
                throw new NoWorkItemsInAppException(NoWorkItemsInApp);
            }
        }

        public void ValidateIfAnyBugsExist(IAllTeams allTeams)
        {
            if ((allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(item => item.GetType() == typeof(Bug))
                            .ToList()
                                .Count() == 0))
            {
                throw new NoBugsInAppException(NoBugsInApp);
            }
        }

        public void ValidateIfAnyStoriesExist(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(item => item.GetType() == typeof(Story))
                            .ToList()
                                .Count() == 0)
            {
                throw new NoStoriesInAppException(NoStoriesInApp);
            }
        }

        public void ValidateIfAnyFeedbacksExist(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(item => item.GetType() == typeof(Feedback))
                            .ToList()
                                .Count() == 0)
            {
                throw new NoFeedbacksInAppException(NoFeedbacksInApp);
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

