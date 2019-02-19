using Autofac;
using System;
using System.Reflection;
using Wim.Core.Contracts;
using Wim.Core.Engine;
using Wim.Core.Engine.EngineOperations;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(WorkItem)))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BugOperations)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Priority)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CreateBugOperation)))
              .AsImplementedInterfaces();

            builder.RegisterType<AllTeams>()
                .As<IAllTeams>().SingleInstance();

            builder.RegisterType<AllMembers>()
              .As<IAllMembers>().SingleInstance();

            builder.RegisterType<AddCommentOperation>()
          .Named<IEngineOperations>("AddComment");

            builder.RegisterType<AddPersonToTeamOperation>()
               .Named<IEngineOperations>("AddPersonToTeam");

            builder.RegisterType<AssignUnassignItemOperation>()
               .Named<IEngineOperations>("AssignUnassignItem");

            builder.RegisterType<ChangeBugPriorityOperation>()
               .Named<IEngineOperations>("ChangeBugPriority");

            builder.RegisterType<ChangeBugSeverityOperation>()
               .Named<IEngineOperations>("ChangeBugSeverity");

            builder.RegisterType<ChangeBugStatusOperation>()
                .Named<IEngineOperations>("ChangeBugStatus");

            builder.RegisterType<ChangeFeedbackRatingOperation>()
               .Named<IEngineOperations>("ChangeFeedbackRating");

            builder.RegisterType<ChangeFeedbackStatusOperation>()
               .Named<IEngineOperations>("ChangeFeedbackStatus");

            builder.RegisterType<ChangeStoryPriorityOperation>()
               .Named<IEngineOperations>("ChangeStoryPriority");

            builder.RegisterType<ChangeStorySizeOperation>()
               .Named<IEngineOperations>("ChangeStorySize");

            builder.RegisterType<ChangeStoryStatusOperation>()
               .Named<IEngineOperations>("ChangeStoryStatus");

            builder.RegisterType<CreateBoardToTeamOperation>()
               .Named<IEngineOperations>("CreateBoard");

            builder.RegisterType<CreateBugOperation>()
              .Named<IEngineOperations>("CreateBug");

            builder.RegisterType<CreateFeedbackOperation>()
              .Named<IEngineOperations>("CreateFeedback");

            builder.RegisterType<CreatePersonOperation>()
              .Named<IEngineOperations>("CreatePerson");

            builder.RegisterType<CreateStoryOperation>()
              .Named<IEngineOperations>("CreateStory");

            builder.RegisterType<CreateTeamOperation>()
              .Named<IEngineOperations>("CreateTeam");

            builder.RegisterType<FilterBugsByAssigneeOperation>()
              .Named<IEngineOperations>("FilterBugsByAssignee");

            builder.RegisterType<FilterBugsByPriorityOperation>()
              .Named<IEngineOperations>("FilterBugsByPriority");

            builder.RegisterType<FilterBugsByStatusOperation>()
                .Named<IEngineOperations>("FilterBugsByStatus");

            builder.RegisterType<FilterBugsOperation>()
                .Named<IEngineOperations>("FilterBugs");

            builder.RegisterType<FilterFeedbacksByStatusOperation>()
                 .Named<IEngineOperations>("FilterFeedbacksByStatus");

            builder.RegisterType<FilterFeedbacksOperation>()
                 .Named<IEngineOperations>("FilterFeedbacksOperation");

            builder.RegisterType<FilterStoriesByAssigneeOperation>()
                 .Named<IEngineOperations>("FilterStoriesByAssignee");

            builder.RegisterType<FilterStoriesByPriorityOperation>()
                .Named<IEngineOperations>("FilterStoriesByPriority");

            builder.RegisterType<FilterStoriesByStatusOperation>()
                .Named<IEngineOperations>("FilterStoriesByStatus");

            builder.RegisterType<FilterStoriesOperation>()
               .Named<IEngineOperations>("FilterStories");

            builder.RegisterType<ListAllWorkItemsOperation>()
               .Named<IEngineOperations>("ListAllWorkItems");

            builder.RegisterType<ShowAllPeopleOperation>()
               .Named<IEngineOperations>("ShowAllPeople");

            builder.RegisterType<ShowAllTeamBoardsOperation>()
               .Named<IEngineOperations>("ShowAllTeamBoards");

            builder.RegisterType< ShowAllTeamMembersOperation>()
               .Named<IEngineOperations>("ShowAllTeamMembers");

            builder.RegisterType<ShowAllTeamsOperation>()
               .Named<IEngineOperations>("ShowAllTeams");

            builder.RegisterType<ShowBoardActivityOperation>()
               .Named<IEngineOperations>("ShowBoardActivity");

            builder.RegisterType<ShowMemberActivityOperation>()
               .Named<IEngineOperations>("ShowMemberActivity");

            builder.RegisterType<ShowTeamActivityOperation>()
               .Named<IEngineOperations>("ShowTeamActivity");

            builder.RegisterType<SortBugsByOperation>()
               .Named<IEngineOperations>("SortBugsBy");

            builder.RegisterType<SortFeedbacksByOperation>()
               .Named<IEngineOperations>("SortFeedbacksBy");

            builder.RegisterType<SortStoriesByOperation>()
               .Named<IEngineOperations>("SortStoriesBy");
            
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Start();
        }
    }
}
