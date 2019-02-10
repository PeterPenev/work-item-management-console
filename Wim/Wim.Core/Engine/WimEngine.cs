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

        private static readonly WimEngine SingleInstance = new WimEngine();

        private readonly IWimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IInputValidator inputValidator;
        private readonly ICommandHelper commandHelper;

        private WimEngine()
        {
            this.factory = new WimFactory();
            this.allMembers = new AllMembers();
            this.allTeams = new AllTeams();
            this.enumParser = new EnumParser();
            this.inputValidator = new InputValidator();
            this.commandHelper = new CommandHelper();
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
            Console.WriteLine(commandHelper.Help);
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

                    var buildFeedbackDescription = new StringBuilder();

                    for (int i = 5; i < command.Parameters.Count; i++)
                    {
                        buildFeedbackDescription.Append(command.Parameters[i] + " ");
                    }

                    var feedbackDescription = buildFeedbackDescription.ToString().Trim();

                    return this.CreateFeedback(feedbackToAdd, teamToAddFeedbackFor, boardToAddFeedbackFor, feedbackRaiting, feedbackStatus, feedbackDescription);


                case "ChangeBugPriority":
                    var teamToChangeBugPriorityFor = command.Parameters[0];
                    var boardToChangeBugPriorityFor = command.Parameters[1];
                    var bugToChangePriorityFor = command.Parameters[2];
                    var newPriority = command.Parameters[3];
                    var authorOfBugPriorityChange = command.Parameters[4];

                    return this.ChangeBugPriority(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor, newPriority, authorOfBugPriorityChange);

                case "ChangeBugSeverity":
                    var teamToChangeBugSeverityFor = command.Parameters[0];
                    var boardToChangeBugSeverityFor = command.Parameters[1];
                    var bugToChangeBugSeverityFor = command.Parameters[2];
                    var newSeverity = command.Parameters[3];
                    var authorOfBugSeverityChange = command.Parameters[4];

                    return this.ChangeBugSeverity(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeBugSeverityFor, newSeverity, authorOfBugSeverityChange);

                case "ChangeBugStatus":
                    var teamToChangeBugStatusFor = command.Parameters[0];
                    var boardToChangeBugStatusFor = command.Parameters[1];
                    var bugToChangeStatusFor = command.Parameters[2];
                    var newStatus = command.Parameters[3];
                    var authorOfBugStatusChange = command.Parameters[4];

                    return this.ChangeBugStatus(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor, newStatus, authorOfBugStatusChange);

                case "ChangeStoryPriority":
                    var teamToChangeStoryPriorityFor = command.Parameters[0];
                    var boardToChangeStoryPriorityFor = command.Parameters[1];
                    var storyToChangePriorityFor = command.Parameters[2];
                    var newStoryPriority = command.Parameters[3];
                    var authorOfStoryPriorityChange = command.Parameters[4];

                    return this.ChangeStoryPriority(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor, storyToChangePriorityFor, newStoryPriority, authorOfStoryPriorityChange);

                case "ChangeStorySize":
                    var teamToChangeStorySizeFor = command.Parameters[0];
                    var boardToChangeStorySizeFor = command.Parameters[1];
                    var storyToChangeSizeFor = command.Parameters[2];
                    var newStorySize = command.Parameters[3];
                    var authorOfStorySizeChange = command.Parameters[4];

                    return this.ChangeStorySize(teamToChangeStorySizeFor, boardToChangeStorySizeFor, storyToChangeSizeFor, newStorySize, authorOfStorySizeChange);

                case "ChangeStoryStatus":
                    var teamToChangeStoryStatusFor = command.Parameters[0];
                    var boardToChangeStoryStatusFor = command.Parameters[1];
                    var storyToChangeStatusFor = command.Parameters[2];
                    var newStoryStatus = command.Parameters[3];
                    var authorOfStoryStatusChange = command.Parameters[4];

                    return this.ChangeStoryStatus(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor, newStoryStatus, authorOfStoryStatusChange);

                case "ChangeFeedbackRating":
                    var teamToChangeFeedbackRatingFor = command.Parameters[0];
                    var boardToChangeFeedbackRatingFor = command.Parameters[1];
                    var feedbackToChangeRatingFor = command.Parameters[2];
                    var newFeedbackRating = command.Parameters[3];
                    var authorOfFeedbackRatingChange = command.Parameters[4];

                    return this.ChangeFeedbackRating(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor, newFeedbackRating, authorOfFeedbackRatingChange);

                case "ChangeFeedbackStatus":
                    var teamToChangeFeedbackStatusFor = command.Parameters[0];
                    var boardToChangeFeedbackStatusFor = command.Parameters[1];
                    var feedbackToChangeStatusFor = command.Parameters[2];
                    var newFeedbackStatus = command.Parameters[3];
                    var authorOfFeedbackStatusChange = command.Parameters[4];
                    return this.ChangeFeedbackStatus(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor, newFeedbackStatus, authorOfFeedbackStatusChange);

                case "AddComment":
                    var teamToAddCommentToWorkItemFor = command.Parameters[0];
                    var boardToAddCommentToWorkItemFor = command.Parameters[1];
                    var itemTypeToAddWorkItemFor = command.Parameters[2];
                    var workitemToAddCommentFor = command.Parameters[3];
                    var authorOfComment = command.Parameters[4];

                    //build comment
                    var buildComment = new StringBuilder();

                    for (int i = 5; i < command.Parameters.Count; i++)
                    {
                        buildComment.Append(command.Parameters[i] + " ");
                    }

                    var commentToAdd = buildComment.ToString().Trim();

                    return this.AddComment(teamToAddCommentToWorkItemFor, boardToAddCommentToWorkItemFor, itemTypeToAddWorkItemFor, workitemToAddCommentFor, authorOfComment, commentToAdd);
                        
                case "AssignUnassignItem":
                    var teamToAssignUnsignBug = command.Parameters[0];
                    var boardToAssignUnsignBug = command.Parameters[1];
                    var typeOfItem = command.Parameters[2];
                    var itemToAssignUnsign = command.Parameters[3];
                    var memberToAssignBug = command.Parameters[4];

                    return this.AssignUnassignItem(teamToAssignUnsignBug, boardToAssignUnsignBug, typeOfItem, itemToAssignUnsign, memberToAssignBug);

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

                case "FilterFeedbacksByStatus":
                    var statusToFilterFeedbackFor = command.Parameters[0];
                    return this.FilterFeedbacksByStatus(statusToFilterFeedbackFor);

                case "SortBugsBy":
                    var factorToSortBugBy = command.Parameters[0];
                    return this.SortBugsBy(factorToSortBugBy);

                case "SortStoriesBy":
                    var factorToSortStoriesBy = command.Parameters[0];
                    return this.SortStoriesBy(factorToSortStoriesBy);

                case "SortFeedbacksBy":
                    var factorToSortFeedbacksBy = command.Parameters[0];
                    return this.SortFeedbacksBy(factorToSortFeedbacksBy);

                
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

        private string CreatePerson(string personName)
        {
            //Validations          
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personName, personTypeForChecking);

            inputValidator.ValdateMemberNameLength(personName);

            inputValidator.ValidateIfPersonExists(allMembers, personName);

            //Operations
            var person = this.factory.CreateMember(personName);
            allMembers.AddMember(person);

            return string.Format(PersonCreated, personName);
        }

        private string ShowAllPeople()
        {
            //Validations
            inputValidator.ValdateIfAnyMembersExist(allMembers);            

            //Operations
            var peopleToDisplay = allMembers.ShowAllMembersToString();
                
            return string.Format(peopleToDisplay);
        }

        private string ShowAllTeams()
        {
            //Validations
            inputValidator.ValdateIfAnyTeamsExist(allTeams);  

            //Operations
            var teamsToDisplay = allTeams.ShowAllTeamsToString();

            return string.Format(teamsToDisplay);
        }

        private string ShowMemberActivityToString(string memberName)
        {
            //Validations
            var inputTypeForChecking = "Member Name";
            inputValidator.IsNullOrEmpty(memberName, inputTypeForChecking);

            inputValidator.ValidateMemberExistance(allMembers, memberName);            

            //Operations
            var selectedMember = this.allMembers.AllMembersList[memberName];
            var memberActivities = selectedMember.ShowMemberActivityToString();

            return string.Format(memberActivities);
        }

        private string CreateTeam(string teamName)
        {
            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValidateIfTeamExists(allTeams, teamName);

            //Operations
            var team = this.factory.CreateTeam(teamName);
            allTeams.AddTeam(team);

            return string.Format(TeamCreated, teamName);
        }

        private string ShowTeamActivityToString(string teamName)
        {
            //Validations
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValdateIfAnyTeamsExist(allTeams);

            inputValidator.ValidateTeamExistance(allTeams, teamName);
            
            //Operations
            var teamToCheckHistoryFor = allTeams.AllTeamsList[teamName];
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString();            

            return string.Format(teamActivityHistory);
        }

        private string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo)
        {
            //Validations
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personToAddToTeam, personTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddPersonTo, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddPersonTo);

            inputValidator.ValidateMemberExistance(allMembers, personToAddToTeam);

            inputValidator.ValidateIfMemberAlreadyInTeam(allTeams, teamToAddPersonTo, personToAddToTeam);

            //Operations
            allTeams.AllTeamsList[teamToAddPersonTo].AddMember(allMembers.AllMembersList[personToAddToTeam]);
            return string.Format(PersonAddedToTeam, personToAddToTeam, teamToAddPersonTo);
        }

        private string ShowAllTeamMembers(string teamToShowMembersFor)
        {
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowMembersFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowMembersFor);

            var allTeamMembersStringResult = allTeams.AllTeamsList[teamToShowMembersFor].ShowAllTeamMembers();
            return string.Format(allTeamMembersStringResult);
        }

        private string CreateBoardToTeam(string boardToAddToTeam, string teamForAddingBoardTo)
        {
            //Validations
            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddToTeam, boardTypeForChecking);

            var teamTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(teamForAddingBoardTo, teamTypeForChecking);

            inputValidator.ValdateBoardNameLength(boardToAddToTeam);

            inputValidator.ValidateTeamExistance(allTeams, teamForAddingBoardTo);

            inputValidator.ValidateBoardAlreadyInTeam(allTeams, boardToAddToTeam, teamForAddingBoardTo);

            //Operations
            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }

        private string ShowAllTeamBoards(string teamToShowBoardsFor)
        {
            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardsFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardsFor);

            inputValidator.ValdateIfBoardsExistInTeam(allTeams, teamToShowBoardsFor);

            //Operations
            var allTeamBoardsResult = allTeams.AllTeamsList[teamToShowBoardsFor].ShowAllTeamBoards();
            return string.Format(allTeamBoardsResult);
        }

        private string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAssignee, IList<string> bugStepsToReproduce, string bugDescription)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugTitle, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddBugFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddBugFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(bugTitle);

            inputValidator.ValdateItemDescriptionLength(bugDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddBugFor);

            inputValidator.ValidateMemberExistance(allMembers, bugAssignee);

            inputValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddBugFor, bugAssignee);

            inputValidator.ValidateBugExistanceInBoard(allTeams, boardToAddBugFor, teamToAddBugFor, bugTitle);

            //Operations
            Priority bugPriorityEnum = this.enumParser.GetPriority(bugPriority);
            Severity bugSeverityEnum = this.enumParser.GetSeverity(bugSeverity);
            IBug bugToAddToCollection = this.factory.CreateBug(bugTitle, bugPriorityEnum, bugSeverityEnum, allMembers.AllMembersList[bugAssignee], bugStepsToReproduce, bugDescription);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddBugFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddBugFor);

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(bugToAddToCollection);

            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddWorkItemIdToMember(bugToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam];
            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee);
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddBugFor];

            allTeams.AllTeamsList[teamToAddBugFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(memberToPutHistoryFor, bugToAddToCollection);
            allTeams.AllTeamsList[teamToAddBugFor].Members.First(member => member.Name == bugAssignee).AddActivityHistoryToMember(bugToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(BugCreated, bugTitle);
        }

        private string CreateStory(string storyTitle, string teamToAddStoryFor, string boardToAddStoryFor, string storyPriority, string storySize, string storyStatus, string storyAssignee, string storyDescription)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyTitle, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddStoryFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddStoryFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(storyTitle);

            inputValidator.ValdateItemDescriptionLength(storyDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddStoryFor);

            inputValidator.ValidateMemberExistance(allMembers, storyAssignee);

            inputValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddStoryFor, storyAssignee);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddStoryFor, teamToAddStoryFor);

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToAddStoryFor, teamToAddStoryFor, storyTitle);

            //Operations
            Priority storyPriorityEnum = this.enumParser.GetPriority(storyPriority);
            Size storySizeEnum = this.enumParser.GetStorySize(storySize);
            StoryStatus storyStatusEnum = this.enumParser.GetStoryStatus(storyStatus);

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
            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackTitle, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddFeedbackFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddFeedbackFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(feedbackTitle);

            inputValidator.ValdateItemDescriptionLength(feedbackDescription);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddFeedbackFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor);

            inputValidator.ValidateFeedbackExistanceInBoard(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor, feedbackTitle);

            var intFeedbackRating = inputValidator.ValidateRatingConversion(feedbackRaiting);            

            //Operations
            FeedbackStatus feedbackStatusEnum = this.enumParser.GetFeedbackStatus(feedbackStatus);

            IFeedback feedbackToAddToCollection = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, intFeedbackRating, feedbackStatusEnum);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddFeedbackFor);

            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(feedbackToAddToCollection);
            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(feedbackToAddToCollection);

            return string.Format(FeedbackCreated, feedbackTitle);
        }

        private string ShowBoardActivityToString(string teamToShowBoardActivityFor, string boardActivityToShow)
        { 
            //Validations
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardActivityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardActivityToShow, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardActivityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardActivityToShow, teamToShowBoardActivityFor);      

            //Operations
            var boardToDisplayActivityFor = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow).FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }

        private string ChangeBugPriority(string teamToChangeBugPriorityFor, string boardToChangeBugPriorityFor, string bugToChangePriorityFor, string priority, string authorOfBugPriorityChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangePriorityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugPriorityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor, bugToChangePriorityFor);

            //Operations
            var newPriorityEnum = enumParser.GetPriority(priority);

            var itemType = "Bug";

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            castedBugForPriorityChange.ChangeBugPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugPriorityFor, authorOfBugPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugPriorityFor, itemType, boardToChangeBugPriorityFor, bugToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, priority);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, priority);

            bugToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, bugToAddActivityFor, priority);

            return string.Format(BugPriorityChanged, bugToChangePriorityFor, newPriorityEnum);

        }

        private string ChangeBugSeverity(string teamToChangeBugSeverityFor, string boardToChangeBugSeverityFor, string bugToChangeSeverityFor, string newSeverity, string authorOfBugSeverityChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeSeverityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugSeverityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugSeverityFor, boardTypeForChecking);

            var severityTypeForChecking = "Severity";
            inputValidator.IsNullOrEmpty(newSeverity, severityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugSeverityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugSeverityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor, bugToChangeSeverityFor);

            //Operations
            var itemType = "Bug";

            var newSeverityEnum = enumParser.GetSeverity(newSeverity);

            var castedBugForPriorityChange = allTeams.FindBugAndCast(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            castedBugForPriorityChange.ChangeBugSeverity(newSeverityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeBugSeverityFor, authorOfBugSeverityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var bugToAddActivityFor = allTeams.FindWorkItem(teamToChangeBugSeverityFor, itemType, boardToChangeBugSeverityFor, bugToChangeSeverityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeBugSeverityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            memberToAddActivityFor.AddActivityHistoryToMember(bugToAddActivityFor, teamToFindIn, boardToAddActivityFor, newSeverity);

            bugToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, bugToAddActivityFor, newSeverity);

            return string.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
        }

        private string ChangeBugStatus(string teamToChangeBugStatusFor, string boardToChangeBugStatusFor, string bugToChangeStatusFor, string newStatus, string authorOfBugStatusChange)
        {
            //Validations
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeStatusFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor);

            inputValidator.ValidateNoSuchBugInBoard(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor, bugToChangeStatusFor);

            //Operations
            var itemType = "Bug";

            var newStatusEnum = enumParser.GetBugStatus(newStatus);

            var castedBugToChangeStatusIn = allTeams.FindBugAndCast(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var bugToChangeStatus = allTeams.FindWorkItem(teamToChangeBugStatusFor, itemType, boardToChangeBugStatusFor, bugToChangeStatusFor);

            var boardToChangeStatusIn = allTeams.FindBoardInTeam(teamToChangeBugStatusFor, boardToChangeBugStatusFor);

            var teamToChangeStatusOfBoardIn = allTeams.AllTeamsList[teamToChangeBugStatusFor];

            var memberToChangeActivityHistoryFor = allTeams.FindMemberInTeam(teamToChangeBugStatusFor, authorOfBugStatusChange);

            castedBugToChangeStatusIn.ChangeBugStatus(newStatusEnum);

            memberToChangeActivityHistoryFor
                .AddActivityHistoryToMember(bugToChangeStatus, teamToChangeStatusOfBoardIn, boardToChangeStatusIn, newStatusEnum);

            boardToChangeStatusIn
                .AddActivityHistoryToBoard(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            bugToChangeStatus
                .AddActivityHistoryToWorkItem(memberToChangeActivityHistoryFor, bugToChangeStatus, newStatusEnum);

            return string.Format(BugStatusChanged, bugToChangeStatusFor, newStatus);
        }

        private string ChangeStoryPriority(string teamToChangeStoryPriorityFor, string boardToChangeStoryPriorityFor, string storyToChangePriorityFor, string newStoryPriority, string authorOfStoryPriorityChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangePriorityFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryPriorityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryPriorityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(newStoryPriority, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryPriorityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryPriorityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor, storyToChangePriorityFor);

            //Operations
            var itemType = "Story";

            var newPriorityEnum = enumParser.GetPriority(newStoryPriority);

            var castedStoryForPriorityChange = allTeams.FindStoryAndCast(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            castedStoryForPriorityChange.ChangeStoryPriority(newPriorityEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryPriorityFor, authorOfStoryPriorityChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryPriorityFor, itemType, boardToChangeStoryPriorityFor, storyToChangePriorityFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryPriorityFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryPriority);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryPriority);

            return string.Format(StoryPriorityChanged, storyToChangePriorityFor, newPriorityEnum);

        }

        private string ChangeStorySize(string teamToChangeStorySizeFor, string boardToChangeStorySizeFor, string storyToChangeSizeFor, string newStorySize, string authorOfStorySizeChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeSizeFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStorySizeFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStorySizeFor, boardTypeForChecking);

            var sizeTypeForChecking = "Size";
            inputValidator.IsNullOrEmpty(newStorySize, sizeTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStorySizeChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStorySizeFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor, storyToChangeSizeFor);

            //Operations
            var itemType = "Story";

            var newSizeEnum = enumParser.GetStorySize(newStorySize);

            var castedStoryForSizeChange = allTeams.FindStoryAndCast(teamToChangeStorySizeFor, boardToChangeStorySizeFor, storyToChangeSizeFor);

            castedStoryForSizeChange.ChangeStorySize(newSizeEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStorySizeFor, authorOfStorySizeChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStorySizeFor, itemType, boardToChangeStorySizeFor, storyToChangeSizeFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStorySizeFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStorySizeFor, boardToChangeStorySizeFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStorySize);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStorySize);

            return string.Format(StoryPriorityChanged, storyToChangeSizeFor, newSizeEnum);
        }

        private string ChangeStoryStatus(string teamToChangeStoryStatusFor, string boardToChangeStoryStatusFor, string storyToChangeStatusFor, string newStoryStatus, string authorOfStoryStatusChange)
        {
            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeStatusFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStoryStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeStoryStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor);

            inputValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor, storyToChangeStatusFor);


            //Operations
            var itemType = "Story";

            var newStatusEnum = enumParser.GetStoryStatus(newStoryStatus);

            var castedStoryForStatusChange = allTeams.FindStoryAndCast(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            castedStoryForStatusChange.ChangeStoryStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryStatusFor, authorOfStoryStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryStatusFor, itemType, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryStatus);

            storyToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            return string.Format(StoryPriorityChanged, storyToChangeStatusFor, newStatusEnum);

        }

        private string ChangeFeedbackRating(string teamToChangeFeedbackRatingFor, string boardToChangeFeedbackRatingFor, string feedbackToChangeRatingFor, string newFeedbackRating, string authorOfFeedbackRatingChange)
        {
            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackToChangeRatingFor, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeFeedbackRatingFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeFeedbackRatingFor, boardTypeForChecking);

            var ratingTypeForChecking = "Rating";
            inputValidator.IsNullOrEmpty(newFeedbackRating, ratingTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfFeedbackRatingChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeFeedbackRatingFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeFeedbackRatingFor, teamToChangeFeedbackRatingFor);

            inputValidator.ValidateNoSuchFeedbackInBoard(allTeams, boardToChangeFeedbackRatingFor, teamToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            var integerRating = inputValidator.ValidateRatingConversion(newFeedbackRating);

            //Operations  
            var itemType = "Feedback";

            var castedFeedbackForRatingChange = allTeams.FindFeedbackAndCast(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            castedFeedbackForRatingChange.ChangeFeedbackRating(integerRating);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeFeedbackRatingFor, authorOfFeedbackRatingChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeFeedbackRatingFor];

            var feedbackToAddActivityFor = allTeams.FindWorkItem(teamToChangeFeedbackRatingFor, itemType, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeFeedbackRatingFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackRating);

            memberToAddActivityFor.AddActivityHistoryToMember(feedbackToAddActivityFor, teamToFindIn, boardToAddActivityFor, newFeedbackRating);

            feedbackToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackRating);

            return string.Format(FeedbackRatingChanged, feedbackToChangeRatingFor, integerRating);
        }

        private string ChangeFeedbackStatus(string teamToChangeFeedbackStatusFor, string boardToChangeFeedbackStatusFor, string feedbackToChangeStatusFor, string newFeedbackStatus, string authorOfFeedbackStatusChange)
        {
            //Validations
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackToChangeStatusFor, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeFeedbackStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeFeedbackStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newFeedbackStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfFeedbackStatusChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeFeedbackStatusFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeFeedbackStatusFor, teamToChangeFeedbackStatusFor);

            inputValidator.ValidateNoSuchFeedbackInBoard(allTeams, boardToChangeFeedbackStatusFor, teamToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            //Operations
            var itemType = "Feedback";

            var newStatusEnum = enumParser.GetFeedbackStatus(newFeedbackStatus);

            var castedFeedbackForStatusChange = allTeams.FindFeedbackAndCast(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            castedFeedbackForStatusChange.ChangeFeedbackStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeFeedbackStatusFor, authorOfFeedbackStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeFeedbackStatusFor];

            var feedbackToAddActivityFor = allTeams.FindWorkItem(teamToChangeFeedbackStatusFor, itemType, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeFeedbackStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(feedbackToAddActivityFor, teamToFindIn, boardToAddActivityFor, newFeedbackStatus);

            feedbackToAddActivityFor.AddActivityHistoryToWorkItem(memberToAddActivityFor, feedbackToAddActivityFor, newFeedbackStatus);

            return string.Format(FeedbackStatusChanged, feedbackToChangeStatusFor, newStatusEnum);
        }

        private string AddComment(string teamToAddCommentToWorkItemFor, string boardToAddCommentToWorkItemFor, string itemTypeToAddWorkItemFor, string workitemToAddCommentFor, string authorOfComment, string commentToAdd)
        {
            //Validations
            var itemTypeForChecking = "Item Title";
            inputValidator.IsNullOrEmpty(workitemToAddCommentFor, itemTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddCommentToWorkItemFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddCommentToWorkItemFor, boardTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfComment, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddCommentToWorkItemFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddCommentToWorkItemFor, teamToAddCommentToWorkItemFor);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateItemExistanceInBoard(allTeams, boardToAddCommentToWorkItemFor, teamToAddCommentToWorkItemFor, workitemToAddCommentFor);

            //Operations
            var workItemToAddCommentTo = allTeams.FindWorkItem(teamToAddCommentToWorkItemFor, itemTypeToAddWorkItemFor, boardToAddCommentToWorkItemFor, workitemToAddCommentFor);

            workItemToAddCommentTo.AddComment(commentToAdd, authorOfComment);

            return string.Format(AddedCommentFor, commentToAdd, authorOfComment, itemTypeToAddWorkItemFor, workitemToAddCommentFor);
        }

        public string AssignUnassignItem(string teamToAssignUnsignItem, string boardToAssignUnsignItem, string itemType, string itemToAssignUnsign, string memberToAssignItem)
        {
            //Validations
            var itemTypeForChecking = "Item Title";
            inputValidator.IsNullOrEmpty(itemToAssignUnsign, itemTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAssignUnsignItem, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAssignUnsignItem, boardTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(memberToAssignItem, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAssignUnsignItem);

            inputValidator.ValidateMemberExistance(allMembers, memberToAssignItem);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToAssignUnsignItem, teamToAssignUnsignItem);

            //Operations
            var itemMemberToAssign = allTeams.FindMemberInTeam(teamToAssignUnsignItem, memberToAssignItem);

            var itemToChangeIn = allTeams.FindWorkItem(teamToAssignUnsignItem, itemType, boardToAssignUnsignItem, itemToAssignUnsign);

            IMember itemMemberBeforeUnssign = null;

            if (itemType == "Bug")
            {
                var typedItem = (Bug)itemToChangeIn;               

                itemMemberBeforeUnssign = typedItem.Assignee;

                typedItem.AssignMemberToBug(itemMemberToAssign);

                itemMemberBeforeUnssign.RemoveWorkItemIdToMember(typedItem.Id);

                itemMemberToAssign.AddWorkItemIdToMember(typedItem.Id);
            }
            else if (itemType == "Story")
            {
                var typedItem = (Story)itemToChangeIn;

                itemMemberBeforeUnssign = typedItem.Assignee;

                typedItem.AssignMemberToStory(itemMemberToAssign);

                itemMemberBeforeUnssign.RemoveWorkItemIdToMember(typedItem.Id);

                itemMemberToAssign.AddWorkItemIdToMember(typedItem.Id);
            }           
                   
            //history
            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAssignUnsignItem].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAssignUnsignItem);

            //add history to board
            allTeams.AllTeamsList[teamToAssignUnsignItem].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryAfterAssignUnsignToBoard(itemType, itemToAssignUnsign, itemMemberToAssign, itemMemberBeforeUnssign);

            //add history to member before unssign
            itemMemberBeforeUnssign.AddActivityHistoryAfterUnsignToMember(itemType, itemToAssignUnsign, itemMemberBeforeUnssign);

            //add history to member after assign
            itemMemberToAssign.AddActivityHistoryAfterAssignToMember(itemType, itemToAssignUnsign, itemMemberToAssign);

            return string.Format(AssignItemTo, itemType, itemToAssignUnsign, boardToAssignUnsignItem, teamToAssignUnsignItem, memberToAssignItem);
        }

        private string ListAllWorkItems()
        {
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            //Operations
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
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
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
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
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
            //Validations
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
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
            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterBugFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var priorityToCheckFor = this.enumParser.GetPriority(priorityToFilterBugFor);

            var filteredBugsByPriority = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();
           
            long workItemCounter = 1;

            if (filteredBugsByPriority.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {priorityToFilterBugFor} Priority in the app yet!");              
            }
            else
            {
                sb.AppendLine($"----ALL BUGS WITH {priorityToFilterBugFor} PRIORITY IN APPLICAITION----");
                foreach (var item in filteredBugsByPriority)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }            

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugsByAssignee(string assigneeToFilterBugFor)
        {
            //Validations
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterBugFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var filteredBugsByAssignee = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.Assignee.Name == assigneeToFilterBugFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();           
            long workItemCounter = 1;

            if (filteredBugsByAssignee.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {assigneeToFilterBugFor} Assignee in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL BUGS ASSIGNED TO MEMBER: {assigneeToFilterBugFor} IN APPLICAITION----");
                foreach (var item in filteredBugsByAssignee)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }            

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterBugsByStatus(string statusToFilterBugFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterBugFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
            var bugStatusToCheckFor = this.enumParser.GetBugStatus(statusToFilterBugFor);

            var filteredBugsByStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Bug))
                            .Select(workItem => (Bug)workItem)
                                .Where(bug => bug.BugStatus == bugStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();            
            long workItemCounter = 1;

            if (filteredBugsByStatus.Count == 0)
            {
                sb.AppendLine($"There are no Bugs with: {statusToFilterBugFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL BUGS WITH {statusToFilterBugFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredBugsByStatus)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }            

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByPriority(string priorityToFilterStoryFor)
        {
            //Validations
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterStoryFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var priorityToCheckFor = this.enumParser.GetPriority(priorityToFilterStoryFor);

            var filteredStoriesByPriority = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(bug => bug.Priority == priorityToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();            
            long workItemCounter = 1;

            if (filteredStoriesByPriority.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {priorityToFilterStoryFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES WITH {priorityToFilterStoryFor} PRIORITY IN APPLICAITION----");
                foreach (var item in filteredStoriesByPriority)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }           

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByAssignee(string assigneeToFilterStoryFor)
        {
            //Validations
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterStoryFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var filteredStoriesByAssignee = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.Assignee.Name == assigneeToFilterStoryFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();            
            long workItemCounter = 1;

            if (filteredStoriesByAssignee.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {assigneeToFilterStoryFor} Assignee in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES ASSIGNED TO MEMBER: {assigneeToFilterStoryFor} IN APPLICAITION----");
                foreach (var item in filteredStoriesByAssignee)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }            

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterStoriesByStatus(string statusToFilterStoryFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterStoryFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var storyStatusToCheckFor = this.enumParser.GetStoryStatus(statusToFilterStoryFor);

            var filteredStoriesbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                .Where(story => story.StoryStatus == storyStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();           
            long workItemCounter = 1;

            if (filteredStoriesbyStatus.Count == 0)
            {
                sb.AppendLine($"There are no Stories with: {statusToFilterStoryFor} Status in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL STORIES WITH {statusToFilterStoryFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredStoriesbyStatus)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }            

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string FilterFeedbacksByStatus(string statusToFilterFeedbacksFor)
        {
            //Validations
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterFeedbacksFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
            var feedbacksStatusToCheckFor = this.enumParser.GetFeedbackStatus(statusToFilterFeedbacksFor);

            var filteredFeedbacksbyStatus = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Feedback))
                            .Select(workItem => (Feedback)workItem)
                                .Where(story => story.FeedbackStatus == feedbacksStatusToCheckFor)
                                    .ToList();


            StringBuilder sb = new StringBuilder();            
            long workItemCounter = 1;

            if (filteredFeedbacksbyStatus.Count == 0)
            {
                sb.AppendLine($"There are No Feedbacks with Staus {statusToFilterFeedbacksFor} in the app yet!");
            }
            else
            {
                sb.AppendLine($"----ALL FEEDBACKS WITH {statusToFilterFeedbacksFor} STATUS IN APPLICAITION----");
                foreach (var item in filteredFeedbacksbyStatus)
                {
                    sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                    workItemCounter++;
                }
                sb.AppendLine("---------------------------------");
            }           
            
            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }

        private string SortBugsBy(string factorToSortBy)
        {
            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

            //Operations
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
            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
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
            else if (factorToSortBy.ToLower() == "size")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Size)
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

        private string SortFeedbacksBy(string factorToSortBy)
        {
            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

            //Operations
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
    }
}
