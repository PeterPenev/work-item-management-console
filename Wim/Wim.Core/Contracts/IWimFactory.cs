using System;
using System.Collections.Generic;
using System.Text;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Contracts
{
    public interface IWimFactory
    {
        ITeam CreateTeam(string name);

        IMember CreateMember(string name, IAllTeams allTeams);

        IBoard CreateBoard(string name);

        IBug CreateBug(string title, Priority priority, Severity severity, IMember asignee, IList<string> stepsToReproduce, string description);

        IStory CreateStory(string title, string description, Priority priority, Size size, StoryStatus storyStatus, IMember assignee);

        IFeedback CreateFeedback(string title, string description, int raiting, FeedbackStatus feedbackStatus);
    }
}
