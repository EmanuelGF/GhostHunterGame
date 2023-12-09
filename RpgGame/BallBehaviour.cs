using Stride.Audio;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;
using System.Threading.Tasks;

namespace RpgGame
{
    public class BallBehaviour : AsyncScript
    {
        public Vector3 direction;
        public const float speed = 0.05f;
        private SoundInstance ghostDiesSound;

        public override async Task Execute()
        {
            var shootBallSound = Content.Load<Sound>("Sounds/GhostDiesSound");
            ghostDiesSound = shootBallSound.CreateInstance();
            ghostDiesSound.Volume = 0.1f;

            Script.AddTask(CheckCollision);
            while (Game.IsRunning)
            {
                // Do stuff every new frame
                await Script.NextFrame();
                var startingPosition = Entity.Transform.Position;
                Entity.Transform.Position = startingPosition + speed * direction;
            }
        }

        private async Task CheckCollision()
        {
            var physics = Entity.Get<RigidbodyComponent>();
            var collider = await physics.NewCollision();

            var affected = collider.ColliderA == physics ? collider.ColliderB.Entity : collider.ColliderA.Entity;

            // Check if affected contains the IsDestructable component.
            var isDestructable = affected.Get<IsDestructableComponent>();
            if (isDestructable != null)
            {
                // remove affected (Ghost)
                isDestructable.Destroy();

                ScoreManager.AddScore(50);

                ghostDiesSound.Play();
            }

            // remove ball.
            Entity.Scene = null;
            Entity.Dispose();
        }
    }
}
