using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;
using Stride.Physics;
using Stride.Rendering.Sprites;

namespace RpgGame
{
    public class SpawnPoint : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private float rotationSpeed = 0.6f;
        private float accumulatedTime = 0;

        public override void Update()
        {
            // Do stuff every new frame
            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
            Entity.Transform.Rotation *= Quaternion.RotationZ(deltaTime * rotationSpeed);

            // Spawn ghosts every 5 seconds.
            accumulatedTime += deltaTime;
            if (accumulatedTime > 5)
            {
                SpawnGhost();
                accumulatedTime = 0;
            }
        }

        private void SpawnGhost()
        {
            var GhostHunter = Entity.Scene.Entities.FirstOrDefault(e => e.Name == "GhostHunter");
            if (GhostHunter == null) return;

            var ghost = new Entity();
            ghost.Name = "Ghost";
            var sprite = ghost.GetOrCreate<SpriteComponent>();
            sprite.SpriteProvider = new SpriteFromSheet
            {
                Sheet = Content.Load<SpriteSheet>("Enemy"),
                CurrentFrame = 0,
            };

            var rigidBody = ghost.GetOrCreate<RigidbodyComponent>();
            rigidBody.RigidBodyType = RigidBodyTypes.Kinematic;
            rigidBody.ColliderShapes.Add(new BoxColliderShapeDesc
            {
                Is2D = true,
                Size = new Vector3(0.6F, 0.8F, 1)
            });
            rigidBody.CollisionGroup = CollisionFilterGroups.CustomFilter2;

            ghost.GetOrCreate<GhostMovement>();
            ghost.GetOrCreate<IsDestructableComponent>();
            ghost.GetOrCreate<GhostCollidesWithGhostHunterBehaviour>();

            ghost.Transform.Position = Entity.Transform.Position;
            Entity.Scene.Entities.Add(ghost);
        }
    }
}
