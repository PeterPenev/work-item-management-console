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
            //AutoFac
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

            builder.RegisterType<CreatePersonOperation>()
               .Named<IEngineOperations>("CreatePerson");

            //containerBuilder.RegisterType<CreateLectureResourceCommand>()
            //   .Named<ICommand>("CreateLectureResource");

            //containerBuilder.RegisterType<CreateTrainerCommand>()
            //   .Named<ICommand>("CreateTrainerResource");

            //containerBuilder.RegisterType<CreateTrainerCommand>()
            //   .Named<ICommand>("CreateTrainerResource");

            var container = builder.Build();

            var engine = container.Resolve<IEngine>();       

            engine.Start();
        }
    }
}
