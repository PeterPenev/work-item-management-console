using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public static class WimEngineExtensionMethods
    {
        public static IWorkItem FindWorkItem(this IAllTeams allTeams, string teamToFindItemFor, string itemType, string boardToFindItemFor, string nameOfWorkItemToFind)
        {
            return allTeams.AllTeamsList[teamToFindItemFor]
                        .Boards.Find(board => board.Name == boardToFindItemFor)
                            .WorkItems.Where(item => item.GetType().Name == itemType).First(workItem => workItem.Title == nameOfWorkItemToFind);
        }

        public static IMember FindMemberInTeam(this IAllTeams allTeams, string teamToFindMemberFor, string nameOfMemberToFind)
        {
            return allTeams.AllTeamsList[teamToFindMemberFor].Members
                                .First(member => member.Name == nameOfMemberToFind);
        }

        public static IBoard FindBoardInTeam(this IAllTeams allTeams, string teamToFindBoardFor, string nameOfBoardToFind)
        {
            return allTeams.AllTeamsList[teamToFindBoardFor]
                                        .Boards.Find(board => board.Name == nameOfBoardToFind);
        }

        public static IBug FindBugAndCast(this IAllTeams allTeams, string teamToFindBoardFor, string boardToFindBugIn, string nameOfBugToCast)
        {
            return allTeams.AllTeamsList[teamToFindBoardFor].Boards
                .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToFindBugIn).WorkItems
                  .Select(item => (IBug)item)
                   .First(bugInSelectedBoard => bugInSelectedBoard.Title == nameOfBugToCast);
        }

        public static IStory FindStoryAndCast(this IAllTeams allTeams, string teamToFindBoardFor, string boardToFindStoryIn, string nameOfStoryToCast)
        {
            return allTeams.AllTeamsList[teamToFindBoardFor].Boards
                .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToFindStoryIn).WorkItems
                  .Select(item => (IStory)item)
                   .First(bugInSelectedBoard => bugInSelectedBoard.Title == nameOfStoryToCast);
        }

        public static IFeedback FindFeedbackAndCast(this IAllTeams allTeams, string teamToFindBoardFor, string boardToFindFeedbackIn, string nameOfFeedbackToCast)
        {
            return allTeams.AllTeamsList[teamToFindBoardFor].Boards
                .Find(boardInSelectedTeam => boardInSelectedTeam.Name == boardToFindFeedbackIn).WorkItems
                  .Select(item => (IFeedback)item)
                   .First(bugInSelectedBoard => bugInSelectedBoard.Title == nameOfFeedbackToCast);
        }

    }
}
