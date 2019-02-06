﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.CustomExceptions;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public class InputValidator
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string PersonExists = "Person with name {0} already exists!";
        private const string PersonCreated = "Person with name {0} was created!";
        private const string NoPeopleInApplication = "There are no people!";
        private const string NoTeamsInApplication = "There are no teams in application!";
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
        private const string NullOrEmptyBugName = "Bug Name cannot be null or empty!";
        private const string BugCreated = "Bug {0} was created!";
        private const string BugAlreadyExists = "Bug with name {0} already exists!";
        private const string BoardDoesNotExist = "Board with name {0} doest not exist!";
        private const string NullOrEmptyStoryName = "Story Name cannot be null or empty!";
        private const string StoryAlreadyExists = "Story with name {0} already exists!";
        private const string StoryCreated = "Story {0} was created!";
        private const string NullOrEmptyFeedbackName = "Feedback Name cannot be null or empty!";
        private const string FeedbackAlreadyExists = "Feedback with name {0} already exists!";
        private const string FeedbackCreated = "Feedback {0} was created!";
        private const string InvalidFeedbackRaiting = "{0} is Invalid Feedback Raiting value!";

        public InputValidator()
        {

        }

        public void ValidateTeamsExistance(IAllTeams allTeams)
        {
            if (allTeams.AllTeamsList.Count == 0)
            {
                throw new TeamNotInAppException(string.Format(NoTeamsInApplication));
            }
        }

        public void ValidateMembersExistance(IAllMembers allMembers)
        {
            if (allMembers.AllMembersList.Count == 0)
            {
                throw new MemberNotInAppException(string.Format(NoTeamsInApplication));
            }
        }
    }
}
