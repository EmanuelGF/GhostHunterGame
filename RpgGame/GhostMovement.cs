using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

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
