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

        //private const string CreamCreated = "Cream with name {0} was created!";
        //private const string ProductAddedToShoppingCart = "Product {0} was added to the shopping cart!";
        //private const string ProductDoesNotExistInShoppingCart = "Shopping cart does not contain product with name {0}!";
        //private const string ProductRemovedFromShoppingCart = "Product {0} was removed from the shopping cart!";
        //private const string TotalPriceInShoppingCart = "${0} total price currently in the shopping cart!";
        private const string InvalidPriorityType = "Invalid Priority type!";
        private const string InvalidStatusType = "Invalid Status type!";
        private const string InvalidStoryStatusType = "Invalid Story Status type!";
        private const string InvalidStorySizeType = "Invalid Story Size type!";
        private const string InvalidFeedbackStatusType = "Invalid Feedback Status type!";
        private const string InvalidFeedbackRaiting = "{0} is Invalid Feedback Raiting value!";


        private static readonly WimEngine SingleInstance = new WimEngine();

        private readonly WimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;

        private WimEngine()
        {
            this.factory = new WimFactory();
            this.allMembers = new AllMembers();
            this.allTeams = new AllTeams();
        }

        public static WimEngine Instance
        {
            get
            {
                return SingleInstance;
            }
        }

        public void Start()
        {
            var commands = this.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        private IList<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentLine = Console.ReadLine();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = Command.Parse(currentLine);
                commands.Add(currentCommand);

                currentLine = Console.ReadLine();
            }

            return commands;
        }

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.ProcessSingleCommand(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {
                case "CreatePerson":
                    var personName = command.Parameters[0];
                    return this.CreatePerson(personName);

                case "ShowAllPeople":
                    return this.ShowAllPeople();

                case "ShowAllTeams":
                    return this.ShowAllTeams();

                case "ShowPersonsActivity":
                    var memberName = command.Parameters[0];
                    return this.ShowMemberActivityToString(memberName);

                case "CreateTeam":
                    var teamName = command.Parameters[0];
                    return this.CreateTeam(teamName);

                case "ShowTeamsActivity":
                    var teamToShowActivityFor = command.Parameters[0];
                    return this.ShowTeamActivityToString(teamToShowActivityFor);

                case "AddPersonToTeam":
                    var personToAddToTeam = command.Parameters[0];
                    var teamForAddingPersonTo = command.Parameters[1];
                    return this.AddPersonToTeam(personToAddToTeam, teamForAddingPersonTo);

                case "ShowAllTeamMembers":
                    var teamToShowMembersFor = command.Parameters[0];

                    return this.ShowAllTeamMembers(teamToShowMembersFor);

                case "CreateBoard":
                    var boardToAddToTeam = command.Parameters[0];
                    var teamForAddingBoardTo = command.Parameters[1];
                    return this.CreateBoardToTeam(boardToAddToTeam, teamForAddingBoardTo);

                case "ShowAllTeamBoards":
                    var teamToShowBoards = command.Parameters[0];
                    return this.ShowAllTeamBoards(teamToShowBoards);

                case "ShowBoardActivity":
                    var team = command.Parameters[0];
                    var boardToShowActivityFor = command.Parameters[1];
                    return this.ShowBoardActivityToString(team, boardToShowActivityFor);

                case "CreateBug":
                    var bugToAdd = command.Parameters[0];
                    var teamToAddBugFor = command.Parameters[1];
                    var boardToAddBugFor = command.Parameters[2];
                    var bugPriority = command.Parameters[3];
                    var bugSeverity = command.Parameters[4];
                    var bugAsignee = command.Parameters[5];

                    var stepsAndDescription = string.Join(' ', command.Parameters);
                    var stepsAndDescriptionArr = stepsAndDescription.Split("!Steps").ToArray();
                    var bugSteps = stepsAndDescriptionArr[1].Split('#').ToList();
                    var bugDescription = stepsAndDescriptionArr[2].ToString().Trim();

                    return this.CreateBug(bugToAdd, teamToAddBugFor, boardToAddBugFor, bugPriority, bugSeverity, bugAsignee, bugSteps, bugDescription);

                case "CreateStory":
                    var storyToAdd = command.Parameters[0];
                    var teamToAddStoryFor = command.Parameters[1];
                    var boardToAddStoryFor = command.Parameters[2];
                    var storyPriority = command.Parameters[3];
                    var storySize = command.Parameters[4];
                    var storyStatus = command.Parameters[5];
                    var storyAssignee = command.Parameters[6];

                    //build story description
                    var buildStoryDescription = new StringBuilder();

                    for (int i = 7; i < command.Parameters.Count; i++)
                    {
                        buildStoryDescription.Append(command.Parameters[i] + " ");
                    }

                    var storyDescription = buildStoryDescription.ToString().Trim();

                    return this.CreateStory(storyToAdd, teamToAddStoryFor, boardToAddStoryFor, storyPriority, storySize, storyStatus, storyAssignee, storyDescription);

                case "CreateFeedback":
                    var feedbackToAdd = command.Parameters[0];
                    var teamToAddFeedbackFor = command.Parameters[1];
                    var boardToAddFeedbackFor = command.Parameters[2];
                    var feedbackRaiting = command.Parameters[3];
                    var feedbackStatus = command.Parameters[4];

                    //build feedback description
                    var buildFeedbackDescription = new StringBuilder();

                    for (int i = 5; i < command.Parameters.Count; i++)
                    {
                        buildFeedbackDescription.Append(command.Parameters[i] + " ");
                    }

                    var feedbackDescription = buildFeedbackDescription.ToString().Trim();

                    return this.CreateFeedback(feedbackToAdd, teamToAddFeedbackFor, boardToAddFeedbackFor, feedbackRaiting, feedbackStatus, feedbackDescription);


                case "ListAllWorkItems":
                    return this.ListAllWorkItems();

                case "FilterBugs":
                    return this.FilterBugs();

                case "FilterStories":
                    return this.FilterStories();

                case "FilterFeedbacks":
                    return this.FilterFeedbacks();

                case "FilterBugsByPriority":
                    var priorityToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByPriority(priorityToFilterBugFor);

                case "FilterBugsByAssignee":
                    var assigneeToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByAssignee(assigneeToFilterBugFor);

                case "FilterBugsByStatus":
                    var statusToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByStatus(statusToFilterBugFor);

                case "FilterStoriesByPriority":
                    var priorityToFilterStoryFor = command.Parameters[0];
                    return this.FilterStoriesByPriority(priorityToFilterStoryFor);

                case "FilterStoriesByAssignee":
                    var assigneeToFilterStoriesFor = command.Parameters[0];
                    return this.FilterStoriesByAssignee(assigneeToFilterStoriesFor);

                case "FilterStoriesByStatus":
                    var statusToFilterStoriesFor = command.Parameters[0];
                    return this.FilterStoriesByStatus(statusToFilterStoriesFor);

                case "FilterFeedbackByStatus":
                    var statusToFilterFeedbackFor = command.Parameters[0];
                    return this.FilterBugsByStatus(statusToFilterFeedbackFor);

                case "SortBugsBy":
                    var factorToSortBugBy = command.Parameters[0];
                    return this.SortBugsBy(factorToSortBugBy);

                case "SortStoriesBy":
                    var factorToSortStoriesBy = command.Parameters[0];
                    return this.SortStoriesBy(factorToSortStoriesBy);

                case "SortFeedbackBy":
                    var factorToSortFeedbackBy = command.Parameters[0];
                    return this.SortFeedbackBy(factorToSortFeedbackBy);

                default:
                    return string.Format(InvalidCommand, command.Name);
            }
        }

        private void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
        }


        //FOR IMPROVING!!!
        private string CreatePerson(string personName)
        {

            var allNames2 = (allMembers.AllMembersList.GetType().GetProperties());
            List<string> allNames = new List<string>();
            foreach (var item in allNames2)
            {
                allNames.Add(item.ToString());
            }

            if (allNames.Contains(personName))
            {
                return string.Format(PersonExists, personName);
            }

            var person = this.factory.CreateMember(personName);
            allMembers.AddMember(person);

            return string.Format(PersonCreated, personName);
        }

        private string ShowAllPeople()
        {

            if (this.allMembers.AllMembersList.Count == 0)
            {
                return string.Format(NoPeopleInApplication);
            }

            var peopleToDisplay = allMembers.ShowAllMembersToString();

            return string.Format(peopleToDisplay);
        }

        private string ShowAllTeams()
        {
            if (this.allTeams.AllTeamsList.Count == 0)
            {
                return string.Format(NoTeamsInApplication);
            }

            var teamsToDisplay = allTeams.ShowAllTeamsToString();

            return string.Format(teamsToDisplay);
        }

        private string ShowMemberActivityToString(string memberName)
        {
            if (string.IsNullOrEmpty(memberName))
            {
                return string.Format(NullOrEmptyMemberName);
            }

            if (!allMembers.AllMembersList.ContainsKey(memberName))
            {
                return string.Format(MemberDoesNotExist);
            }

            var selectedMember = this.allMembers.AllMembersList[memberName];
            var memberActivities = selectedMember.ShowMemberActivityToString();

            return string.Format(memberActivities);
        }

        private string CreateTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (this.allTeams.AllTeamsList.ContainsKey(teamName))
            {
                return string.Format(TeamNameExists);
            }

            var team = this.factory.CreateTeam(teamName);
            allTeams.AddTeam(team);

            return string.Format(TeamCreated, teamName);
        }

        private string ShowTeamActivityToString(string team)
        {
            if (string.IsNullOrEmpty(team))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(team))
            {
                return string.Format(TeamDoesNotExist);
            }

            var teamToCheckHistoryFor = allTeams.AllTeamsList[team];
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString();

            return string.Format(teamActivityHistory);
        }


        private string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo)
        {
            if (string.IsNullOrEmpty(personToAddToTeam))
            {
                return string.Format(NullOrEmptyMemberName);
            }

            if (string.IsNullOrEmpty(teamToAddPersonTo))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allMembers.AllMembersList.ContainsKey(personToAddToTeam))
            {
                return string.Format(MemberDoesNotExist);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamToAddPersonTo))
            {
                return string.Format(TeamDoesNotExist);
            }

            allTeams.AllTeamsList[teamToAddPersonTo].AddMember(allMembers.AllMembersList[personToAddToTeam]);
            return string.Format(PersonAddedToTeam, personToAddToTeam, teamToAddPersonTo);
        }

        private string ShowAllTeamMembers(string teamToShowMembersFor)
        {
            if (string.IsNullOrEmpty(teamToShowMembersFor))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamToShowMembersFor))
            {
                return string.Format(TeamDoesNotExist);
            }

            var allTeamMembersStringResult = allTeams.AllTeamsList[teamToShowMembersFor].ShowAllTeamMembers();
            return string.Format(allTeamMembersStringResult);
        }

        private string CreateBoardToTeam(string boardToAddToTeam, string teamForAddingBoardTo)
        {

            if (string.IsNullOrEmpty(boardToAddToTeam))
            {
                return string.Format(NullOrEmptyBoardName);
            }

            if (string.IsNullOrEmpty(teamForAddingBoardTo))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamForAddingBoardTo))
            {
                return string.Format(TeamDoesNotExist, teamForAddingBoardTo);
            }

            var boardMatches = allTeams.AllTeamsList[teamForAddingBoardTo].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddToTeam);

            if (boardMatches.Count() > 0)
            {
                return string.Format(BoardAlreadyExists, boardToAddToTeam);
            }

            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }


        private string ShowAllTeamBoards(string teamToShowBoards)
        {
            if (string.IsNullOrEmpty(teamToShowBoards))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamToShowBoards))
            {
                return string.Format(TeamDoesNotExist);
            }

            if (allTeams.AllTeamsList[teamToShowBoards].Boards.Count() == 0)
            {
                return string.Format(NoBoardsInTeam);
            }

            var allTeamBoardsResult = allTeams.AllTeamsList[teamToShowBoards].ShowAllTeamBoards();
            return string.Format(allTeamBoardsResult);
        }

        private string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAsignee, IList<string> bugStepsToReproduce, string bugDescription)
        {
            if (string.IsNullOrEmpty(bugTitle))
            {
                return string.Format(NullOrEmptyBugName);
            }

            if (string.IsNullOrEmpty(teamToAddBugFor))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!this.allTeams.AllTeamsList.ContainsKey(teamToAddBugFor))
            {
                return string.Format(TeamDoesNotExist, teamToAddBugFor);
            }

            if (string.IsNullOrEmpty(boardToAddBugFor))
            {
                return string.Format(NullOrEmptyBoardName);
            }

            var boardMatches = allTeams.AllTeamsList[teamToAddBugFor].Boards
              .Any(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor);

            if (boardMatches == false)
            {
                return string.Format(BoardDoesNotExist, boardToAddBugFor);
            }

            var boardToCheckBugFor = allTeams.AllTeamsList[teamToAddBugFor].Boards
                .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor).First();

            var doesBugExistInBoard = boardToCheckBugFor.WorkItems
                .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Bug)).Any(bugThatExists => bugThatExists.Title == bugTitle);

            if (doesBugExistInBoard)
            {
                return string.Format(BugAlreadyExists, boardToAddBugFor);
            }


            Priority bugPriorityEnum = this.GetPriority(bugPriority);
            Severity bugSeverityEnum = this.GetSeverity(bugSeverity);
            IBug bugToAddToCollection = this.factory.CreateBug(bugTitle, bugPriorityEnum, bugSeverityEnum, allMembers.AllMembersList[bugAsignee], bugStepsToReproduce, bugDescription);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddBugFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddBugFor);

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(bugToAddToCollection);

            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAsignee).AddWorkItemIdToMember(bugToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAsignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor];

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, bugToAddToCollection);
            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAsignee).AddActivityHistoryToMember(bugToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(BugCreated, bugTitle);
        }

        private string CreateStory(string storyTitle, string teamToAddStoryFor, string boardToAddStoryFor, string storyPriority, string storySize, string storyStatus, string storyAssignee, string storyDescription)

        {
            if (string.IsNullOrEmpty(storyTitle))
            {
                return string.Format(NullOrEmptyStoryName);
            }

            if (string.IsNullOrEmpty(teamToAddStoryFor))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (string.IsNullOrEmpty(boardToAddStoryFor))
            {
                return string.Format(NullOrEmptyBoardName);
            }

            if (!this.allTeams.AllTeamsList.ContainsKey(teamToAddStoryFor))
            {
                return string.Format(TeamDoesNotExist, teamToAddStoryFor);
            }


            var boardMatches = allTeams.AllTeamsList[teamToAddStoryFor].Boards
              .Any(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddStoryFor);

            if (boardMatches == false)
            {
                return string.Format(BoardDoesNotExist, boardToAddStoryFor);
            }

            var boardToCheckStoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Boards
                .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddStoryFor).First();

            var doesStoryExistInBoard = boardToCheckStoryFor.WorkItems
                .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Story)).Any(storyThatExists => storyThatExists.Title == storyTitle);

            if (doesStoryExistInBoard)
            {
                return string.Format(StoryAlreadyExists, boardToAddStoryFor);
            }


            Priority storyPriorityEnum = this.GetPriority(storyPriority);
            Size storySizeEnum = this.GetStorySize(storySize);
            StoryStatus storyStatusEnum = this.GetStoryStatus(storyStatus);


            IStory storyToAddToCollection = this.factory.CreateStory(storyTitle, storyDescription, storyPriorityEnum, storySizeEnum, storyStatusEnum, allMembers.AllMembersList[storyAssignee]);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddStoryFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddStoryFor);

            allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(storyToAddToCollection);

            allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee).AddWorkItemIdToMember(storyToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor];

            allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, storyToAddToCollection);
            allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee).AddActivityHistoryToMember(storyToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(StoryCreated, storyTitle);
        }

        private string CreateFeedback(string feedbackTitle, string teamToAddFeedbackFor, string boardToAddFeedbackFor, string feedbackRaiting, string feedbackStatus, string feedbackDescription)
        {
            if (string.IsNullOrEmpty(feedbackTitle))
            {
                return string.Format(NullOrEmptyFeedbackName);
            }

            if (string.IsNullOrEmpty(teamToAddFeedbackFor))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (string.IsNullOrEmpty(boardToAddFeedbackFor))
            {
                return string.Format(NullOrEmptyBoardName);
            }

            if (!this.allTeams.AllTeamsList.ContainsKey(teamToAddFeedbackFor))
            {
                return string.Format(TeamDoesNotExist, teamToAddFeedbackFor);
            }


            var boardMatches = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards
              .Any(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddFeedbackFor);

            if (boardMatches == false)
            {
                return string.Format(BoardDoesNotExist, boardToAddFeedbackFor);
            }

            var boardToCheckFeedbackFor = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards
                .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddFeedbackFor).First();

            var doesFeedbackExistInBoard = boardToCheckFeedbackFor.WorkItems
                .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Feedback)).Any(feedbackThatExists => feedbackThatExists.Title == feedbackTitle);

            if (doesFeedbackExistInBoard)
            {
                return string.Format(FeedbackAlreadyExists, boardToAddFeedbackFor);
            }

            //check feedback raiting
            int intFeedbackRaiting;
            bool feedbackRaitingParseResult = int.TryParse(feedbackRaiting, out intFeedbackRaiting);
            if (!feedbackRaitingParseResult)
            {
                return string.Format(InvalidFeedbackRaiting, feedbackRaiting);

            }

            //parse feedbackStatusEnum
            FeedbackStatus feedbackStatusEnum = this.GetFeedbackStatus(feedbackStatus);


            IFeedback feedbackToAddToCollection = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, intFeedbackRaiting, feedbackStatusEnum);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddFeedbackFor);

            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(feedbackToAddToCollection);
            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(feedbackToAddToCollection);

            return string.Format(FeedbackCreated, feedbackTitle);
        }

        private string ShowBoardActivityToString(string teamToShowBoardActivityFor, string boardActivityToShow)
        {
            if (string.IsNullOrEmpty(teamToShowBoardActivityFor))
            {
                return string.Format(NullOrEmptyTeamName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamToShowBoardActivityFor))
            {
                return string.Format(TeamDoesNotExist);
            }

            if (string.IsNullOrEmpty(boardActivityToShow))
            {
                return string.Format(NullOrEmptyMemberName);
            }

            var boardMatches = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow);

            if (boardMatches.Count() == 0)
            {
                return string.Format(BoardAlreadyExists, boardActivityToShow);
            }

            var boardToDisplayActivityFor = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow).FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }

        private string ListAllWorkItems()
        {
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL WORK ITEMS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugs()
        {
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Bug)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL BUGS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStories()
        {
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Story)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL STORIES IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterFeedbacks()
        {
            var AllWorkItems = allTeams.AllTeamsList.Values.SelectMany(x => x.Boards).SelectMany(x => x.WorkItems).Where(x => x.GetType() == typeof(Feedback)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----ALL FEEDBACKS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in AllWorkItems)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugsByPriority(string priorityToFilterBugFor)
        {
            var priorityToCheckFor = GetPriority(priorityToFilterBugFor);

            var filteredBugsByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL BUGS WITH {priorityToFilterBugFor} PRIORITY IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredBugsByStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugsByAssignee(string assigneeToFilterBugFor)
        {
            var filteredBugsByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Assignee.Name == assigneeToFilterBugFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL BUGS ASSIGNED TO MEMBER: {assigneeToFilterBugFor} IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredBugsByStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugsByStatus(string statusToFilterBugFor)
        {
            var bugStatusToCheckFor = GetBugStatus(statusToFilterBugFor);

            var filteredBugsByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.BugStatus == bugStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL BUGS WITH {statusToFilterBugFor} STATUS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredBugsByStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByPriority(string priorityToFilterStoryFor)
        {
            var priorityToCheckFor = GetPriority(priorityToFilterStoryFor);

            var filteredStoriesByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL STORIES WITH {priorityToFilterStoryFor} PRIORITY IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredStoriesByStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByAssignee(string assigneeToFilterBugFor)
        {
            var filteredStoriesByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.Assignee.Name == assigneeToFilterBugFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL STORIES ASSIGNED TO MEMBER: {assigneeToFilterBugFor} IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredStoriesByStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByStatus(string statusToFilterStoryFor)
        {
            var storyStatusToCheckFor = GetStoryStatus(statusToFilterStoryFor);

            var filteredStoriesbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.StoryStatus == storyStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL STORIES WITH {statusToFilterStoryFor} STATUS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredStoriesbyStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterFeedbacksByStatus(string statusToFilterFeedbacksFor)
        {
            var feedbacksStatusToCheckFor = GetFeedbackStatus(statusToFilterFeedbacksFor);

            var filteredFeedbacksbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                .Where(story => story.FeedbackStatus == feedbacksStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL FEEDBACKS WITH {statusToFilterFeedbacksFor} STATUS IN APPLICAITION----");
            long workItemCounter = 1;
            foreach (var item in filteredFeedbacksbyStatus)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string SortBugsBy(string factorToSortBy)
        {
            var filteredBugs = new List<Bug>();
            if (factorToSortBy.ToLower() == "title")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "priority")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Priority)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "severity")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.Severity)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredBugs = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                  .OrderBy(bugToOrder => bugToOrder.BugStatus)
                                        .ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL BUGS IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredBugs)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title}");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string SortStoriesBy(string factorToSortBy)
        {
            var filteredStories = new List<Story>();
            if (factorToSortBy.ToLower() == "title")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "priority")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Priority)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.StoryStatus)
                                        .ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL STORIES IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredStories)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string SortFeedbackBy(string factorToSortBy)
        {
            var filteredFeedbacks = new List<Feedback>();
            if (factorToSortBy.ToLower() == "title")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "rating")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Rating)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredFeedbacks = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.FeedbackStatus)
                                        .ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL FEEDBACKS IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredFeedbacks)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }



        private Priority GetPriority(string priorityString)
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

        private Severity GetSeverity(string severityString)
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

        private BugStatus GetBugStatus(string bugStatusString)
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

        private StoryStatus GetStoryStatus(string storyStatusString)
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

        private Size GetStorySize(string storySizeString)
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

        private FeedbackStatus GetFeedbackStatus(string feedbackStatusString)
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

        //Internal Use Only !
        private string IsPersonAssigned(string personName)
        {
            var personToCheckFor = allMembers.AllMembersList[personName];

            var result = personToCheckFor.FindIfMemberIsAssigned(allTeams.AllTeamsList);

            return string.Format(result.ToString());
        }
    }
}
