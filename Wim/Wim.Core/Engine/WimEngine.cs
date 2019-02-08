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
        private const string PersonExists = "Person with name {0} already exists!";

        private const string BugPriorityChanged = "Bug {0} priority is changed to {1}";
        private const string BugSeverityChanged = "Bug {0} severity is changed to {1}";
        private const string BugStatusChanged = "Bug {0} status is changed to {1}";

        private const string StoryPriorityChanged = "Story {0} priority is changed to {1}";
        private const string StorySizeChanged = "Story {0} size is changed to {1}";
        private const string StoryStatusChanged = "Story {0} status is changed to{1}";

        private const string FeedbackRatingChanged = "Feedback {0} rating is changed to {1}";
        private const string FeedbackStatusChanged = "Feedback {0} status is changed to {1}";

        private const string AddedCommentFor = "Comment {0} with author {1} is added to {2} with name: {3}.";

        private const string AssignBugTo = "Bug {0} on the board {1} of the team {2} was assign to the member {3}!";

        private static readonly WimEngine SingleInstance = new WimEngine();

        private readonly WimFactory factory;
        private readonly IAllMembers allMembers;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly InputValidator inputValidator;

        private WimEngine()
        {
            this.factory = new WimFactory();
            this.allMembers = new AllMembers();
            this.allTeams = new AllTeams();
            this.enumParser = new EnumParser();
            this.inputValidator = new InputValidator();
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
                    var workitemToAddCommentFor = command.Parameters[2];
                    var authorOfComment = command.Parameters[3];

                    //build comment
                    var buildComment = new StringBuilder();

                    for (int i = 4; i < command.Parameters.Count; i++)
                    {
                        buildComment.Append(command.Parameters[i] + " ");
                    }

                    var commentToAdd = buildComment.ToString().Trim();

                    return this.AddComment(teamToAddCommentToWorkItemFor, boardToAddCommentToWorkItemFor, workitemToAddCommentFor, authorOfComment, commentToAdd);

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

                case "SortFeedbackBy":
                    var factorToSortFeedbackBy = command.Parameters[0];
                    return this.SortFeedbackBy(factorToSortFeedbackBy);

                case "AssignUnassignBug":
                    var teamToAssignUnsignBug = command.Parameters[0];
                    var boardToAssignUnsignBug = command.Parameters[1];
                    var bugToAssignUnsign = command.Parameters[2];
                    var memberToAssignBug = command.Parameters[3];

                    return this.AssignUnassignBug(teamToAssignUnsignBug, boardToAssignUnsignBug, bugToAssignUnsign, memberToAssignBug);

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
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personName, personTypeForChecking);

            inputValidator.ValidateIfPersonExists(allMembers, personName);

            var person = this.factory.CreateMember(personName);
            allMembers.AddMember(person);

            return string.Format(PersonCreated, personName);
        }

        private string ShowAllPeople()
        {
            inputValidator.ValdateIfAnyMembersExist(allMembers);            

            var peopleToDisplay = allMembers.ShowAllMembersToString();

            return string.Format(peopleToDisplay);
        }

        private string ShowAllTeams()
        {
            inputValidator.ValdateIfAnyTeamsExist(allTeams);  

            var teamsToDisplay = allTeams.ShowAllTeamsToString();

            return string.Format(teamsToDisplay);
        }

        private string ShowMemberActivityToString(string memberName)
        {
            var inputTypeForChecking = "Member Name";
            inputValidator.IsNullOrEmpty(memberName, inputTypeForChecking);

            inputValidator.ValidateMemberExistance(allMembers, memberName);            

            var selectedMember = this.allMembers.AllMembersList[memberName];
            var memberActivities = selectedMember.ShowMemberActivityToString();

            return string.Format(memberActivities);
        }

        private string CreateTeam(string teamName)
        {
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValidateIfTeamExists(allTeams, teamName);

            var team = this.factory.CreateTeam(teamName);
            allTeams.AddTeam(team);

            return string.Format(TeamCreated, teamName);
        }

        private string ShowTeamActivityToString(string teamName)
        {
            var inputTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamName, inputTypeForChecking);

            inputValidator.ValdateIfAnyTeamsExist(allTeams);

            inputValidator.ValidateTeamExistance(allTeams, teamName);
            
            var teamToCheckHistoryFor = allTeams.AllTeamsList[teamName];
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString();            

            return string.Format(teamActivityHistory);
        }


        private string AddPersonToTeam(string personToAddToTeam, string teamToAddPersonTo)
        {
            var personTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(personToAddToTeam, personTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddPersonTo, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddPersonTo);

            inputValidator.ValidateMemberExistance(allMembers, personToAddToTeam);            

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
            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddToTeam, boardTypeForChecking);

            var teamTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(teamForAddingBoardTo, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamForAddingBoardTo);

            inputValidator.ValidateBoardExistance(allTeams, boardToAddToTeam, teamForAddingBoardTo);

            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }


        private string ShowAllTeamBoards(string teamToShowBoardsFor)
        {
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardsFor, teamTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardsFor);

            inputValidator.ValdateIfBoardsExistInTeam(allTeams, teamToShowBoardsFor);

            var allTeamBoardsResult = allTeams.AllTeamsList[teamToShowBoardsFor].ShowAllTeamBoards();
            return string.Format(allTeamBoardsResult);
        }

        private string CreateBug(string bugTitle, string teamToAddBugFor, string boardToAddBugFor, string bugPriority, string bugSeverity, string bugAsignee, IList<string> bugStepsToReproduce, string bugDescription)
        {
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugTitle, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddBugFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddBugFor, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddBugFor);

            inputValidator.ValidateBugExistanceInBoard(allTeams, boardToAddBugFor, teamToAddBugFor, bugTitle);

            Priority bugPriorityEnum = this.enumParser.GetPriority(bugPriority);
            Severity bugSeverityEnum = this.enumParser.GetSeverity(bugSeverity);
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
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyTitle, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddStoryFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddStoryFor, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddStoryFor);

            inputValidator.ValidateBoardExistance(allTeams, boardToAddStoryFor, teamToAddStoryFor);

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToAddStoryFor, teamToAddStoryFor, storyTitle);

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
            var feedbackTypeForChecking = "Feedback Title";
            inputValidator.IsNullOrEmpty(feedbackTitle, feedbackTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddFeedbackFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddFeedbackFor, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToAddFeedbackFor);

            inputValidator.ValidateFeedbackExistanceInBoard(allTeams, boardToAddFeedbackFor, teamToAddFeedbackFor, feedbackTitle);

            var intFeedbackRating = inputValidator.ValidateRatingConversion(feedbackRaiting);            

            FeedbackStatus feedbackStatusEnum = this.enumParser.GetFeedbackStatus(feedbackStatus);

            IFeedback feedbackToAddToCollection = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, intFeedbackRating, feedbackStatusEnum);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddFeedbackFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddFeedbackFor);

            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddWorkitemToBoard(feedbackToAddToCollection);
            allTeams.AllTeamsList[teamToAddFeedbackFor].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryToBoard(feedbackToAddToCollection);

            return string.Format(FeedbackCreated, feedbackTitle);
        }

        private string ShowBoardActivityToString(string teamToShowBoardActivityFor, string boardActivityToShow)
        {           
            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToShowBoardActivityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardActivityToShow, boardTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToShowBoardActivityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardActivityToShow, teamToShowBoardActivityFor);      


            var boardToDisplayActivityFor = allTeams.AllTeamsList[teamToShowBoardActivityFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardActivityToShow).FirstOrDefault();

            var boardActivityToString = boardToDisplayActivityFor.ShowBoardActivityToString();
            return string.Format(boardActivityToString);
        }

        private string ChangeBugPriority(string teamToChangeBugPriorityFor, string boardToChangeBugPriorityFor, string bugToChangePriorityFor, string priority, string authorOfBugPriorityChange)
        {
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

            inputValidator.ValidateBugNotInBoard(allTeams, boardToChangeBugPriorityFor, teamToChangeBugPriorityFor, bugToChangePriorityFor);

            var newPriorityEnum = enumParser.GetPriority(priority);

            allTeams.AllTeamsList[teamToChangeBugPriorityFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeBugPriorityFor).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToChangePriorityFor)
                  .ChangeBugPriority(newPriorityEnum);

            //Add to member activity history
            allTeams.AllTeamsList[teamToChangeBugPriorityFor].Members
                .Find(member => member.Name == authorOfBugPriorityChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                        .Boards.Find(board => board.Name == boardToChangeBugPriorityFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangePriorityFor),
                                allTeams.AllTeamsList[teamToChangeBugPriorityFor],
                                    allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                                        .Boards.Find(board => board.Name == boardToChangeBugPriorityFor), newPriorityEnum);

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                .Boards.Find(board => board.Name == boardToChangeBugPriorityFor)
                 .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                  .Members.Find(member => member.Name == authorOfBugPriorityChange),
                   allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                        .Boards.Find(board => board.Name == boardToChangeBugPriorityFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangePriorityFor), newPriorityEnum);

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                .Boards.Find(board => board.Name == boardToChangeBugPriorityFor)
                .WorkItems.Find(item => item.Title == bugToChangePriorityFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                  .Members.Find(member => member.Name == authorOfBugPriorityChange),
                   allTeams.AllTeamsList[teamToChangeBugPriorityFor]
                        .Boards.Find(board => board.Name == boardToChangeBugPriorityFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangePriorityFor), newPriorityEnum);


            return string.Format(BugPriorityChanged, bugToChangePriorityFor, newPriorityEnum);
        }

        private string ChangeBugSeverity(string teamToChangeBugSeverityFor, string boardToChangeBugSeverityFor, string bugToChangeSeverityFor, string newSeverity, string authorOfBugSeverityChange)
        {
            var bugTypeForChecking = "Bug Title";
            inputValidator.IsNullOrEmpty(bugToChangeSeverityFor, bugTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeBugSeverityFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeBugSeverityFor, boardTypeForChecking);

            var priorityTypeForChecking = "Severity";
            inputValidator.IsNullOrEmpty(newSeverity, priorityTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfBugSeverityChange, authorTypeForChecking);

            inputValidator.ValidateTeamExistance(allTeams, teamToChangeBugSeverityFor);

            inputValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor);

            inputValidator.ValidateBugNotInBoard(allTeams, boardToChangeBugSeverityFor, teamToChangeBugSeverityFor, bugToChangeSeverityFor);

            var newSeverityEnum = enumParser.GetSeverity(newSeverity);

            allTeams.AllTeamsList[teamToChangeBugSeverityFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeBugSeverityFor).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToChangeSeverityFor)
                  .ChangeBugSeverity(newSeverityEnum);


            //Add to member activity history
            allTeams.AllTeamsList[teamToChangeBugSeverityFor].Members
                .Find(member => member.Name == authorOfBugSeverityChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                        .Boards.Find(board => board.Name == boardToChangeBugSeverityFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangeSeverityFor),
                                allTeams.AllTeamsList[teamToChangeBugSeverityFor],
                                    allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                                        .Boards.Find(board => board.Name == boardToChangeBugSeverityFor));

            //Add to board activity history

            allTeams.AllTeamsList[teamToChangeBugSeverityFor]
               .Boards.Find(board => board.Name == boardToChangeBugSeverityFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                 .Members.Find(member => member.Name == authorOfBugSeverityChange),
                  allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                       .Boards.Find(board => board.Name == boardToChangeBugSeverityFor)
                           .WorkItems.Find(workItem => workItem.Title == bugToChangeSeverityFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                .Boards.Find(board => board.Name == boardToChangeBugSeverityFor)
                .WorkItems.Find(item => item.Title == bugToChangeSeverityFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                  .Members.Find(member => member.Name == authorOfBugSeverityChange),
                   allTeams.AllTeamsList[teamToChangeBugSeverityFor]
                        .Boards.Find(board => board.Name == boardToChangeBugSeverityFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangeSeverityFor), newSeverityEnum);

            // .Add.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
            return string.Format(BugSeverityChanged, bugToChangeSeverityFor, newSeverityEnum);
        }

        private string ChangeBugStatus(string teamToChangeBugStatusFor, string boardToChangeBugStatusFor, string bugToChangeStatusFor, string newStatus, string authorOfBugStatusChange)
        {
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

            inputValidator.ValidateBugNotInBoard(allTeams, boardToChangeBugStatusFor, teamToChangeBugStatusFor, bugToChangeStatusFor);

            var newStatusEnum = enumParser.GetBugStatus(newStatus);

            allTeams.AllTeamsList[teamToChangeBugStatusFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeBugStatusFor).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToChangeStatusFor)
                  .ChangeBugStatus(newStatusEnum);

            //Add To Member Activity History
            allTeams.AllTeamsList[teamToChangeBugStatusFor].Members
                .Find(member => member.Name == authorOfBugStatusChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeBugStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeBugStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangeStatusFor),
                                allTeams.AllTeamsList[teamToChangeBugStatusFor],
                                    allTeams.AllTeamsList[teamToChangeBugStatusFor]
                                        .Boards.Find(board => board.Name == boardToChangeBugStatusFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeBugStatusFor]
               .Boards.Find(board => board.Name == boardToChangeBugStatusFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeBugStatusFor]
                 .Members.Find(member => member.Name == authorOfBugStatusChange),
                  allTeams.AllTeamsList[teamToChangeBugStatusFor]
                       .Boards.Find(board => board.Name == boardToChangeBugStatusFor)
                           .WorkItems.Find(workItem => workItem.Title == bugToChangeStatusFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeBugStatusFor]
                .Boards.Find(board => board.Name == boardToChangeBugStatusFor)
                .WorkItems.Find(item => item.Title == bugToChangeStatusFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeBugStatusFor]
                  .Members.Find(member => member.Name == authorOfBugStatusChange),
                   allTeams.AllTeamsList[teamToChangeBugStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeBugStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == bugToChangeStatusFor), newStatusEnum);


            return string.Format(BugStatusChanged, bugToChangeStatusFor, newStatus);
        }

        private string ChangeStoryPriority(string teamToChangeStoryPriorityFor, string boardToChangeStoryPriorityFor, string storyToChangePriorityFor, string newStoryPriority, string authorOfStoryPriorityChange)
        {
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

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToChangeStoryPriorityFor, teamToChangeStoryPriorityFor, storyToChangePriorityFor);

            var newPriorityEnum = enumParser.GetPriority(newStoryPriority);

            allTeams.AllTeamsList[teamToChangeStoryPriorityFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeStoryPriorityFor).WorkItems
                .Select(item => (IStory)item)
                 .First(storyInSelectedBoard => storyInSelectedBoard.Title == storyToChangePriorityFor)
                  .ChangeStoryPriority(newPriorityEnum);


            //Add to member activity history
            allTeams.AllTeamsList[teamToChangeStoryPriorityFor].Members
                .Find(member => member.Name == authorOfStoryPriorityChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                        .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangePriorityFor),
                                allTeams.AllTeamsList[teamToChangeStoryPriorityFor],
                                    allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                                        .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
               .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                 .Members.Find(member => member.Name == authorOfStoryPriorityChange),
                  allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                       .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor)
                           .WorkItems.Find(workItem => workItem.Title == storyToChangePriorityFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor)
                .WorkItems.Find(item => item.Title == storyToChangePriorityFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                  .Members.Find(member => member.Name == authorOfStoryPriorityChange),
                   allTeams.AllTeamsList[teamToChangeStoryPriorityFor]
                        .Boards.Find(board => board.Name == boardToChangeStoryPriorityFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangePriorityFor), newPriorityEnum);

            return string.Format(StoryPriorityChanged, storyToChangePriorityFor, newPriorityEnum);
        }

        private string ChangeStorySize(string teamToChangeStorySizeFor, string boardToChangeStorySizeFor, string storyToChangeSizeFor, string newStorySize, string authorOfStorySizeChange)
        {
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

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToChangeStorySizeFor, teamToChangeStorySizeFor, storyToChangeSizeFor);

            var newSizeEnum = enumParser.GetStorySize(newStorySize);

            allTeams.AllTeamsList[teamToChangeStorySizeFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeStorySizeFor).WorkItems
                .Select(item => (IStory)item)
                 .First(storyInSelectedBoard => storyInSelectedBoard.Title == storyToChangeSizeFor)
                  .ChangeStorySize(newSizeEnum);

            //Addto member activity history
            allTeams.AllTeamsList[teamToChangeStorySizeFor].Members
                .Find(member => member.Name == authorOfStorySizeChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeStorySizeFor]
                        .Boards.Find(board => board.Name == boardToChangeStorySizeFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangeSizeFor),
                                allTeams.AllTeamsList[teamToChangeStorySizeFor],
                                    allTeams.AllTeamsList[teamToChangeStorySizeFor]
                                        .Boards.Find(board => board.Name == boardToChangeStorySizeFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeStorySizeFor]
               .Boards.Find(board => board.Name == boardToChangeStorySizeFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeStorySizeFor]
                 .Members.Find(member => member.Name == authorOfStorySizeChange),
                  allTeams.AllTeamsList[teamToChangeStorySizeFor]
                       .Boards.Find(board => board.Name == boardToChangeStorySizeFor)
                           .WorkItems.Find(workItem => workItem.Title == storyToChangeSizeFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeStorySizeFor]
                .Boards.Find(board => board.Name == boardToChangeStorySizeFor)
                .WorkItems.Find(item => item.Title == storyToChangeSizeFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeStorySizeFor]
                  .Members.Find(member => member.Name == authorOfStorySizeChange),
                   allTeams.AllTeamsList[teamToChangeStorySizeFor]
                        .Boards.Find(board => board.Name == boardToChangeStorySizeFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangeSizeFor), newSizeEnum);

            return string.Format(StorySizeChanged, storyToChangeSizeFor, newSizeEnum);
        }

        private string ChangeStoryStatus(string teamToChangeStoryStatusFor, string boardToChangeStoryStatusFor, string storyToChangeStatusFor, string newStoryStatus, string authorOfStoryStatusChange)
        {
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

            inputValidator.ValidateStoryExistanceInBoard(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor, storyToChangeStatusFor);

            var newStatusEnum = enumParser.GetStoryStatus(newStoryStatus);

            allTeams.AllTeamsList[teamToChangeStoryStatusFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeStoryStatusFor).WorkItems
                .Select(item => (IStory)item)
                 .First(storyInSelectedBoard => storyInSelectedBoard.Title == storyToChangeStatusFor)
                  .ChangeStoryStatus(newStatusEnum);


            // Add to member activity history
            allTeams.AllTeamsList[teamToChangeStoryStatusFor].Members
                .Find(member => member.Name == authorOfStoryStatusChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeStoryStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangeStatusFor),
                                allTeams.AllTeamsList[teamToChangeStoryStatusFor],
                                    allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                                        .Boards.Find(board => board.Name == boardToChangeStoryStatusFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeStoryStatusFor]
               .Boards.Find(board => board.Name == boardToChangeStoryStatusFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                 .Members.Find(member => member.Name == authorOfStoryStatusChange),
                  allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                       .Boards.Find(board => board.Name == boardToChangeStoryStatusFor)
                           .WorkItems.Find(workItem => workItem.Title == storyToChangeStatusFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                .Boards.Find(board => board.Name == boardToChangeStoryStatusFor)
                .WorkItems.Find(item => item.Title == storyToChangeStatusFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                  .Members.Find(member => member.Name == authorOfStoryStatusChange),
                   allTeams.AllTeamsList[teamToChangeStoryStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeStoryStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == storyToChangeStatusFor), newStatusEnum);


            return string.Format(StoryStatusChanged, storyToChangeStatusFor, newStoryStatus);
        }

        private string ChangeFeedbackRating(string teamToChangeFeedbackRatingFor, string boardToChangeFeedbackRatingFor, string feedbackToChangeRatingFor, string newFeedbackRating, string authorOfFeedbackRatingChange)
        {
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

            var newRatingInteger = inputValidator.ValidateRatingConversion(newFeedbackRating);

            allTeams.AllTeamsList[teamToChangeFeedbackRatingFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeFeedbackRatingFor).WorkItems
                .Select(item => (IFeedback)item)
                 .First(storyInSelectedBoard => storyInSelectedBoard.Title == feedbackToChangeRatingFor)
                  .ChangeFeedbackRating(newRatingInteger);

            //Add to member activity history
            allTeams.AllTeamsList[teamToChangeFeedbackRatingFor].Members
                .Find(member => member.Name == authorOfFeedbackRatingChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                        .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor)
                            .WorkItems.Find(workItem => workItem.Title == feedbackToChangeRatingFor),
                                allTeams.AllTeamsList[teamToChangeFeedbackRatingFor],
                                    allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                                        .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
               .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                 .Members.Find(member => member.Name == authorOfFeedbackRatingChange),
                  allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                       .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor)
                           .WorkItems.Find(workItem => workItem.Title == feedbackToChangeRatingFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor)
                .WorkItems.Find(item => item.Title == feedbackToChangeRatingFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                  .Members.Find(member => member.Name == authorOfFeedbackRatingChange),
                   allTeams.AllTeamsList[teamToChangeFeedbackRatingFor]
                        .Boards.Find(board => board.Name == boardToChangeFeedbackRatingFor)
                            .WorkItems.Find(workItem => workItem.Title == feedbackToChangeRatingFor), newRatingInteger);

            return string.Format(FeedbackRatingChanged, feedbackToChangeRatingFor, newFeedbackRating);
        }

        private string ChangeFeedbackStatus(string teamToChangeFeedbackStatusFor, string boardToChangeFeedbackStatusFor, string feedbackToChangeStatusFor, string newFeedbackStatus, string authorOfFeedbackStatusChange)
        {
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

            inputValidator.ValidateFeedbackExistanceInBoard(allTeams, boardToChangeFeedbackStatusFor, teamToChangeFeedbackStatusFor, feedbackToChangeStatusFor);

            var newStatusEnum = enumParser.GetFeedbackStatus(newFeedbackStatus);

            allTeams.AllTeamsList[teamToChangeFeedbackStatusFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToChangeFeedbackStatusFor).WorkItems
                .Select(item => (IFeedback)item)
                 .First(feedbackInSelectedBoard => feedbackInSelectedBoard.Title == feedbackToChangeStatusFor)
                  .ChangeFeedbackStatus(newStatusEnum);

            //Add to member activity history
            allTeams.AllTeamsList[teamToChangeFeedbackStatusFor].Members
                .Find(member => member.Name == authorOfFeedbackStatusChange)
                    .AddActivityHistoryToMember(allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == feedbackToChangeStatusFor),
                                allTeams.AllTeamsList[teamToChangeFeedbackStatusFor],
                                    allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                                        .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor));

            //Add to board activity history
            allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
               .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor)
                .AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                 .Members.Find(member => member.Name == authorOfFeedbackStatusChange),
                  allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                       .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor)
                           .WorkItems.Find(workItem => workItem.Title == feedbackToChangeStatusFor));

            //Add to WorkItem Activity History
            allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor)
                .WorkItems.Find(item => item.Title == feedbackToChangeStatusFor)
                 .AddActivityHistoryToWorkItem(allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                  .Members.Find(member => member.Name == authorOfFeedbackStatusChange),
                   allTeams.AllTeamsList[teamToChangeFeedbackStatusFor]
                        .Boards.Find(board => board.Name == boardToChangeFeedbackStatusFor)
                            .WorkItems.Find(workItem => workItem.Title == feedbackToChangeStatusFor), newStatusEnum);

            return string.Format(FeedbackStatusChanged, feedbackToChangeStatusFor, newFeedbackStatus);
        }

        private string AddComment(string teamToAddCommentToWorkItemFor, string boardToAddCommentToWorkItemFor, string workitemToAddCommentFor, string authorOfComment, string commentToAdd)
        {
            allTeams.AllTeamsList[teamToAddCommentToWorkItemFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddCommentToWorkItemFor).WorkItems
                 .First(workitemInSelectedBoard => workitemInSelectedBoard.Title == workitemToAddCommentFor)
                  .AddComment(commentToAdd, authorOfComment);

            var workItemType = allTeams.AllTeamsList[teamToAddCommentToWorkItemFor].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddCommentToWorkItemFor).WorkItems
                 .First(workitemInSelectedBoard => workitemInSelectedBoard.Title == workitemToAddCommentFor).GetType().Name;

            return string.Format(AddedCommentFor, commentToAdd, authorOfComment, workItemType, workitemToAddCommentFor);
        }

        private string ListAllWorkItems()
        {
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

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
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

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
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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
            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

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
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterBugFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

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
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterBugFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

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
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterBugFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

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
            var priorityTypeForChecking = "Priority";
            inputValidator.IsNullOrEmpty(priorityToFilterStoryFor, priorityTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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
            var assigneeTypeForChecking = "Assignee";
            inputValidator.IsNullOrEmpty(assigneeToFilterStoryFor, assigneeTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterStoryFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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
            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(statusToFilterFeedbacksFor, statusTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

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
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyBugsExist(allTeams);

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
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

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
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyFeedbacksExist(allTeams);

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

        public string AssignUnassignBug(string teamToAssignUnsignBug, string boardToAssignUnsignBug, string bugToAssignUnsign, string memberToAssignBug)
        {
            //if (string.IsNullOrEmpty(teamToAssignUnsignBug))
            //{
            //    return string.Format(NullOrEmptyTeamName);
            //}

            //if (string.IsNullOrEmpty(boardToAssignUnsignBug))
            //{
            //    return string.Format(NullOrEmptyBoardName);
            //}

            //if (string.IsNullOrEmpty(bugToAssignUnsign))
            //{
            //    return string.Format(NullOrEmptyBugName);
            //}

            //if (string.IsNullOrEmpty(memberToAssignBug))
            //{
            //    return string.Format(NullOrEmptyMemberName);
            //}

            //if (!this.allTeams.AllTeamsList.ContainsKey(teamToAssignUnsignBug))
            //{
            //    return string.Format(TeamDoesNotExist, teamToAssignUnsignBug);
            //}

            //var doesMemberExistInTeam = allTeams.AllTeamsList[teamToAssignUnsignBug].Members.Any(member => member.Name == memberToAssignBug);

            //if (!doesMemberExistInTeam)
            //{
            //    return string.Format(MemberToAssignDoesNotExist, memberToAssignBug);
            //}

            //var boardToAssignBugForMatches = allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
            //.Any(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug);

            //if (boardToAssignBugForMatches == false)
            //{
            //    return string.Format(BoardDoesNotExist, boardToAssignUnsignBug);
            //}

            //var boardToAssignBugFor = allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
            //.Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).FirstOrDefault();

            //var doesBugExistInBoard = boardToAssignBugFor.WorkItems
            //    .Where(boardInSelectedTeam => boardInSelectedTeam.GetType() == typeof(Bug)).Any(bugThatExists => bugThatExists.Title == bugToAssignUnsign);

            //if (!doesBugExistInBoard)
            //{
            //    return string.Format(BugDoesNotExist, bugToAssignUnsign);
            //}

            var bugID = allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToAssignUnsign).Id;

            var bugMemberBeforeUnssign = allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToAssignUnsign).Assignee;

            var bugMemberToAssignBug = allTeams.AllTeamsList[teamToAssignUnsignBug].Members
                 .Find(member => member.Name == memberToAssignBug);

            //change Bug assignee value
            allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToAssignUnsign)
                 .AssignMemberToBug(allTeams.AllTeamsList[teamToAssignUnsignBug].Members
                 .Find(member => member.Name == memberToAssignBug));

            //remove workItem from list of member
            allMembers.AllMembersList.First(X => X.Value.Name == bugMemberBeforeUnssign.Name).Value.
            RemoveWorkItemIdToMember(allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToAssignUnsign).Id);

            //add workItem to list of member
            allTeams.AllTeamsList[teamToAssignUnsignBug].Members
                 .Find(member => member.Name == memberToAssignBug).AddWorkItemIdToMember(allTeams.AllTeamsList[teamToAssignUnsignBug].Boards
              .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAssignUnsignBug).WorkItems
                .Select(item => (IBug)item)
                 .First(bugInSelectedBoard => bugInSelectedBoard.Title == bugToAssignUnsign).Id);

            //history
            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAssignUnsignBug].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAssignUnsignBug);

            //add history to board
            allTeams.AllTeamsList[teamToAssignUnsignBug].Boards[indexOfBoardInSelectedTeam].AddActivityHistoryAfterAssignUnsignToBoard(bugToAssignUnsign, bugMemberToAssignBug, bugMemberBeforeUnssign);

            //add history to member before unssign
            bugMemberBeforeUnssign.AddActivityHistoryAfterUnsignToMember(bugToAssignUnsign, bugMemberBeforeUnssign);

            //add history to member after assign
            bugMemberToAssignBug.AddActivityHistoryAfterAssignToMember(bugToAssignUnsign, bugMemberToAssignBug);

            return string.Format(AssignBugTo, bugToAssignUnsign, boardToAssignUnsignBug, teamToAssignUnsignBug, memberToAssignBug);
        }
    }
}
