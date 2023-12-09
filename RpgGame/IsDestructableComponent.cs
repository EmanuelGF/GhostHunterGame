using Stride.Engine;

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
