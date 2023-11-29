using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Physics;

namespace RpgGame
{
    public class GhostCollidesWithGhostHunterBehaviour : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            Script.AddTask(CheckCollisionWithGhost);
            while (Game.IsRunning)
            {
                // Do stuff every new frame
                await Script.NextFrame();
            }
        }

        private async Task CheckCollisionWithGhost()
        {
            var physics = Entity.Get<RigidbodyComponent>();
            var collider = await physics.NewCollision();

            var affected = collider.ColliderA == physics ? collider.ColliderB.Entity : collider.ColliderA.Entity;

            if ( affected.Name == "GhostHunter")
            {
                affected.Scene = null;
                affected.Dispose();
            }

        }
    }
}
