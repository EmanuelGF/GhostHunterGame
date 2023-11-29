using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    public class IsDestructableComponent : EntityComponent
    {
        public void Destroy()
        {
            // Initialization of the script.
            Entity.Scene = null;
            // Remove from memory.
            Entity.Dispose();
        }
    }
}
