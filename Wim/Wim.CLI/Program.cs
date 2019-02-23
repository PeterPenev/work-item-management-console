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
            var autofacBuilder = new AutofacBuilder();

            var autofacContainer = autofacBuilder.RegisterContainer();

            var engine = autofacContainer.Resolve<IEngine>();

            engine.Start();
        }
    }
}
