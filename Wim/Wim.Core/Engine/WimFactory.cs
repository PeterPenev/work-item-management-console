using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine
{
    public class WimFactory : IWimFactory
    {
        public ITeam CreateTeam(string name, IMemberOpertaions memberOpertaions)
        {
            return new Team(name, memberOpertaions);
        }

        public IMember CreateMember(string name, IAllTeams allTeams)
        {
            return new Member(name, allTeams);
        }

        public IBoard CreateBoard(string name)
        {
            return new Board(name);
        }

        public IBug CreateBug(string title, Priority priority, Severity severity, IMember asignee, IList<string> stepsToReproduce, string description)
        {
            return new Bug(title, priority, severity, asignee, stepsToReproduce, description);
        }

        public IStory CreateStory(string title, string description, Priority priority, Size size, StoryStatus storyStatus, IMember assignee)
        {
            return new Story(title, description, priority, size, storyStatus, assignee);
        }

        public IFeedback CreateFeedback(string title, string description, int raiting, FeedbackStatus feedbackStatus)
        {
            return new Feedback(title, description, raiting, feedbackStatus);
        }
    }
}
