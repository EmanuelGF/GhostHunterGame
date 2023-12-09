using Stride.Engine;
using System.Linq;

namespace RpgGame
{
    public class GhostMovement : SyncScript
    {
        private Entity GhostHunter;

        public override void Update()
        {
            GhostHunter = Entity.Scene.Entities.FirstOrDefault(e => e.Name == "GhostHunter");
            if (GhostHunter == null) return;

            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
            var direction = GhostHunter.Transform.Position - Entity.Transform.Position;
            direction.Normalize();
            Entity.Transform.Position += direction * 0.8f * deltaTime;
        }
    }
}
