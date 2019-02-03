using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
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
        private const string StoryCreated = "Story in Team {0} was created!";
        private const string NullOrEmptyBugName = "Bug Name cannot be null or empty!";
        private const string BugCreated = "Bug {0} was created!";
        private const string BugAlreadyExists = "Bug with name {0} already exists!";
        //private const string CreamCreated = "Cream with name {0} was created!";
        //private const string ProductAddedToShoppingCart = "Product {0} was added to the shopping cart!";
        //private const string ProductDoesNotExistInShoppingCart = "Shopping cart does not contain product with name {0}!";
        //private const string ProductRemovedFromShoppingCart = "Product {0} was removed from the shopping cart!";
        //private const string TotalPriceInShoppingCart = "${0} total price currently in the shopping cart!";
        //private const string InvalidGenderType = "Invalid gender type!";
        //private const string InvalidUsageType = "Invalid usage type!";
        //private const string InvalidScentType = "Invalid scent type!";


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

                case "ShowBoardActivityToString":
                    var team = command.Parameters[0];
                    var boardToShowHistory = command.Parameters[1];
                    return this.ShowBoardActivityToString(boardActivityToShow);

                //case "TotalPrice":
                //    return this.shoppingCart.ProductList.Any() ? string.Format(TotalPriceInShoppingCart, this.shoppingCart.TotalPrice()) : $"No product in shopping cart!";

                case "CreateBug":
                    var bugToAdd = command.Parameters[0];
                    var boardToAddBugFor = command.Parameters[1];
                    var teamToAddBugFor = command.Parameters[2];

                    return this.CreateBug(bugToAdd,teamToAddBugFor,boardToAddBugFor);

                //InternalUseOnly
                case "IsPersonAssigned":
                    var personName2 = command.Parameters[0];

                    return this.IsPersonAssigned(personName2);

                  
                case "CreateStory":
                    var teamToAddStoryFor = command.Parameters[0];
                    var boardToAddStoryFor = command.Parameters[1];

                    return this.CreateStory(personName2);

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

            var peopleToDisplay = allMembers.ShowAllMembersToString(allMembers.AllMembersList);

            return string.Format(peopleToDisplay);
        }

        private string ShowAllTeams()
        {
            if (this.allTeams.AllTeamsList.Count == 0)
            {
                return string.Format(NoTeamsInApplication);
            }

            var teamsToDisplay = allTeams.ShowAllTeamsToString(allTeams.AllTeamsList);

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
            var memberActivities = selectedMember.ShowMemberActivityToString(selectedMember.ActivityHistory);

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
            var teamActivityHistory = teamToCheckHistoryFor.ShowTeamActivityToString(teamToCheckHistoryFor.Members);

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

        private string CreateBug(string bugToAdd, string teamToAddBugFor, string boardToAddBugFor)
        {
            if (string.IsNullOrEmpty(bugToAdd))
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

            var boardMatches = allTeams.AllTeamsList[boardToAddBugFor].Boards
              .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor);

            if (boardMatches.Count() > 0)
            {
                return string.Format(BoardAlreadyExists, boardToAddBugFor);
            }

            var boardToCheckBugFor = allTeams.AllTeamsList[teamToAddBugFor].Boards
                .Where(boardInSelectedTeam => boardInSelectedTeam.Name == boardToAddBugFor).First();

            var doesBugExistInBoard = boardToCheckBugFor.WorkItems
                .Where(boardInSelectedTeam => boardInSelectedTeam.GetType().Name == "Bug")
                    .Select(bug => bug.Title == bugToAdd).First();

            if (doesBugExistInBoard)
            {
                return string.Format(BugAlreadyExists, boardToAddBugFor);
            }

            //var bug = this.factory.CreateBoard(boardToAddToTeam);
            //allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BugCreated, boardToAddBugFor);
        }

        private string ShowBoardActivityToString(string team, string boardActivityToShow)
        {
            if (string.IsNullOrEmpty(team))
            {
                return string.Format(NullOrEmptyTeamName); 
            }

            if (!allTeams.AllTeamsList.ContainsKey(team))
            {
                return string.Format(TeamDoesNotExist);
            }

            if(string.IsNullOrEmpty(boardActivityToShow))
            {
                return string.Format(NullOrEmptyMemberName);
            }

            if(!)

        }

        //    var product = this.products[productName];
        //    this.shoppingCart.AddProduct(product);

     
        //private GenderType GetGender(string genderAsString)
        //{
        //    switch (genderAsString.ToLower())
        //    {
        //        case "men":
        //            return GenderType.Men;
        //        case "women":
        //            return GenderType.Women;
        //        case "unisex":
        //            return GenderType.Unisex;
        //        default:
        //            throw new InvalidOperationException(InvalidGenderType);
        //    }
        //}

        //private Scent GetScent(string scentAsString)
        //{
        //    switch (scentAsString.ToLower())
        //    {
        //        case "lavander":
        //            return Scent.Lavender;
        //        case "vanilla":
        //            return Scent.Vanilla;
        //        case "rose":
        //            return Scent.Rose;
        //        default:
        //            throw new InvalidOperationException(InvalidScentType);
        //    }
        //}

        //private UsageType GetUsage(string usageAsString)
        //{
        //    switch (usageAsString.ToLower())
        //    {
        //        case "everyday":
        //            return UsageType.EveryDay;
        //        case "medical":
        //            return UsageType.Medical;
        //        default:
        //            throw new InvalidOperationException(InvalidUsageType);
        //    }
        //}

        private string CreateStory(string teamToAddStoryTo, string boardToAddStoryTo)
        {
            if (string.IsNullOrEmpty(teamToAddStoryTo))
            {
                return string.Format(NullOrEmptyTeamName); 
            }

            if (string.IsNullOrEmpty(boardToAddStoryTo))
            {
                return string.Format(NullOrEmptyBoardName);
            }

            if (!allTeams.AllTeamsList.ContainsKey(teamToAddStoryTo))
            {
                return string.Format(TeamDoesNotExist);
            }

            if (allTeams.AllTeamsList[teamToAddStoryTo].Boards.Count() == 0)
            {
                return string.Format(NoBoardsInTeam);
            }

            var allTeamBoardsResult = allTeams.AllTeamsList[teamToAddStoryTo].ShowAllTeamBoards();
            return string.Format(StoryCreated, teamToAddStoryTo);
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
