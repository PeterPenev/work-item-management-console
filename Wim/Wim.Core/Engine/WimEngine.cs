using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public sealed class WimEngine : IEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";

        private const string PersonCreated = "Person with name {0} was created!";
        private const string TeamCreated = "Team with name {0} was created!";
        private const string PersonAddedToTeam = "Person {0} was added to team {1}!";
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";
        private const string BugCreated = "Bug {0} was created!";
        private const string StoryCreated = "Story {0} was created!";
        private const string FeedbackCreated = "Feedback {0} was created!";     

        private const string BugPriorityChanged = "Bug {0} priority is changed to {1}";
        private const string BugSeverityChanged = "Bug {0} severity is changed to {1}";
        private const string BugStatusChanged = "Bug {0} status is changed to {1}";

        private const string StoryPriorityChanged = "Story {0} priority is changed to {1}";
        private const string StorySizeChanged = "Story {0} size is changed to {1}";
        private const string StoryStatusChanged = "Story {0} status is changed to{1}";

        private const string FeedbackRatingChanged = "Feedback {0} rating is changed to {1}";
        private const string FeedbackStatusChanged = "Feedback {0} status is changed to {1}";

        private const string AddedCommentFor = "Comment {0} with author {1} is added to {2} with name: {3}.";
        private const string AssignItemTo = "{0} with name: {1} on board {2} part of team {3} was assigned to member {4}!";

        private readonly IWimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IInputValidator inputValidator;
        private readonly ICommandHelper commandHelper;
        private readonly IWimCommandReader commandReader;

        public WimEngine(IWimFactory factory, 
            IAllMembers allMembers, 
            IAllTeams allTeams, 
            EnumParser enumParser, 
            IInputValidator inputValidator, 
            ICommandHelper commandHelper,
            IWimCommandReader commandReader)
        {
            this.factory = factory;
            this.allMembers = allMembers;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.inputValidator = inputValidator;
            this.commandHelper = commandHelper;
        }

        public void Start()
        {
            Console.WriteLine(commandHelper.Help);
            var commands = commandReader.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }                             
    }
}
