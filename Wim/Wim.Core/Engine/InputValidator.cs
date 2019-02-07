﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.CustomExceptions;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public class InputValidator
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string PersonExists = "Person with name {0} already exists!";
        private const string PersonCreated = "Person with name {0} was created!";
        private const string NullOrEmptyInput = "{0} cannot be null or empty!";
        private const string NoSuchMemberInApplication = "There is no member with {0} name in the Application!";
        private const string NoSuchTeamInApplication = "There is no team with {0} name in the Application!";
        private const string NoMembersInApplication = "There are no Members in the Application yet!";
        private const string NoTeamsInApplication = "There are no Teams in the Application yet!";
        private const string BugAlreadyExists = "Bug with name {0} in Board: {1} part of Team {2} already exists!";
        private const string StoryAlreadyExists = "Story with name {0} in Board: {1} part of Team {2} already exists!";
        private const string FeedbackAlreadyExists = "Feedback with name {0} in Board: {1} part of Team {2} already exists!";



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
                throw new ArgumentNullException(string.Format(NoTeamsInApplication));
            }
        }

        public void ValdateIfAnyTeamsExist(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Count == 0)
            {
                throw new NoTeamsInAppException(string.Format(NoTeamsInApplication));
            }
        }

        public void ValdateIfAnyMembersExist(IAllMembers allMembers)
        {
            if (allMembers.AllMembersList.Count == 0)
            {
                throw new NoMembersInAppException(string.Format(NoMembersInApplication));
            }
        }

        public void ValdateIfBoardsExistInTeam(IAllTeams allTeams, string teamToShowBoardsFor)
        {
            if (allTeams.AllTeamsList[teamToShowBoardsFor].Boards.Count() == 0)
            {
                throw new NoBoardsInTeamException(string.Format(NoBoardsInTeam));
            }
        }

        public void ValidateMemberExistance(IAllMembers allMembers, string memberName)
        {
            if (!allMembers.AllMembersList.ContainsKey(memberName))
            {
                throw new MemberNotInAppException(string.Format(NoSuchMemberInApplication));
            }
        }

        public void ValidateTeamExistance(IAllTeams allTeams, string teamName)
        {
            if (!allTeams.AllTeamsList.ContainsKey(teamName))
            {
                throw new TeamNotInAppException(string.Format(NoSuchTeamInApplication));
            }
        }



        public void ValidateBoardExistance(IAllTeams allTeams, string boardToAddToTeam, string teamForAddingBoardTo)
        {
            var boardMatches = allTeams.AllTeamsList[teamForAddingBoardTo].Boards
             .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddToTeam);

            if (boardMatches.Count() > 0)
            {
                throw new BoardNotInAppException(string.Format(BoardAlreadyExists, boardToAddToTeam));
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
                throw new BugAlreadyInBoardException(string.Format(BugAlreadyExists, bugTitle, boardToAddBugFor, teamToAddBugFor));
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
                throw new StoryAlreadyInBoardException(string.Format(StoryAlreadyExists, storyTitle, boardToAddStoryFor, teamToAddStoryFor));
            }
        }

        public void ValidateFeedbackExistanceInBoard(IAllTeams allTeams, string boardToAddFeedbackFor, string teamToAddFeedbackFor, string feedbackTitle)
        {
            var boardToCheckFeedbackFor = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards
                   .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddFeedbackFor).First();

            var doesFeedbackExistInBoard = boardToCheckFeedbackFor.WorkItems
                   .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Feedback)).Any(feedbackThatExists => feedbackThatExists.Title == feedbackTitle);

            if (doesFeedbackExistInBoard)
            {
                throw new FeedbackAlreadyInBoardException(string.Format(FeedbackAlreadyExists, feedbackTitle, boardToAddFeedbackFor, teamToAddFeedbackFor));
            }
        }
    }

 
}

