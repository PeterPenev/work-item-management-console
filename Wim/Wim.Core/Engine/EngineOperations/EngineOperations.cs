using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine.EngineOperations
{
    public abstract class EngineOperations : IEngineOperations
    {
        public abstract void Execute();
    }
}
