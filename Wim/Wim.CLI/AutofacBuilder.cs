using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Wim.Core.Contracts;
using Wim.Core.Engine.EngineOperations;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations;

namespace Wim.CLI
{
    public class AutofacBuilder
    {
        public IContainer RegisterContainer()
        {
            var builder = new ContainerBuilder();

            Assembly commandsAssembly = Assembly.GetAssembly(typeof(AddCommentOperation));

            RegisterTypes(builder);           

            RegisterCommandsWithStrings(builder, commandsAssembly);

            var container = builder.Build();

            return container;
        }

        public void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(WorkItem)))
               .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BugOperations)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Priority)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CreateBugOperation)))
              .AsImplementedInterfaces();

            builder.RegisterType<AllTeams>()
              .As<IAllTeams>()
                    .SingleInstance();

            builder.RegisterType<AllMembers>()
              .As<IAllMembers>()
                    .SingleInstance();
        }

        public void RegisterCommandsWithStrings(ContainerBuilder builder, Assembly commandsAssembly)
        {
            var commandTypes = commandsAssembly.DefinedTypes
               .Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(IEngineOperations)))
               .ToList();

            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType())
                  .Named<IEngineOperations>(commandType.Name.Replace("Operation", ""));
            }
        }
    }
}
