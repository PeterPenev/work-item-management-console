namespace Wim.Core.Engine.EngineOperationsContracts
{
    public interface IShowOperations
    {
        string ShowAllPeople();

        string ShowAllTeams();

        string ShowMemberActivityToString(string memberName);

        string ShowTeamActivityToString(string teamName);

        string ShowAllTeamMembers(string teamToShowMembersFor);

        string ShowAllTeamBoards(string teamToShowBoardsFor);

        string ShowBoardActivityToString(string teamToShowBoardActivityFor, string boardActivityToShow);

        string ListAllWorkItems();        
    }
}
