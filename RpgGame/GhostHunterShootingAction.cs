using Stride.Audio;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Graphics;
using Stride.Input;
using Stride.Physics;
using Stride.Rendering.Sprites;

namespace RpgGame
{
    public class GhostHunterShootingAction : SyncScript
    {
        private bool isShooting;
        public SpriteSheet itemSpriteSheet;
        private SoundInstance shootBallInstance;

        public override void Start()
        {
            var shootBallSound = Content.Load<Sound>("MagicBallSound");
            shootBallInstance = shootBallSound.CreateInstance();
        }

        public override void Update()
        {
            var direction = Entity.Get<GhostHunterControls>().direction;

            if (Input.HasKeyboard)
            {
                bool isSpaceDown = Input.IsKeyPressed(Keys.Space);
                if (isSpaceDown && !isShooting)
                {
                    if (direction == Direction.Up) ShootBall(new Vector3(0, 1, 0));
                    if (direction == Direction.Down) ShootBall(new Vector3(0, -1, 0));
                    if (direction == Direction.Left) ShootBall(new Vector3(-1, 0, 0));
                    if (direction == Direction.Right) ShootBall(new Vector3(1, 0, 0));
                }
                isShooting = isSpaceDown;
            }
        }

        private void ShootBall(Vector3 direction)
        {
            var ball = new Entity();

            var sprite = ball.GetOrCreate<SpriteComponent>();
            sprite.SpriteProvider = new SpriteFromSheet()
            {
                Sheet = itemSpriteSheet,
                CurrentFrame = 17
            };

            var rigidBody = ball.GetOrCreate<RigidbodyComponent>();
            rigidBody.RigidBodyType = RigidBodyTypes.Kinematic;
            rigidBody.ColliderShapes.Add(new BoxColliderShapeDesc
            {
                Is2D = true,
                Size = new Vector3(0.3F, 0.3F, 1)
            });
            rigidBody.CollisionGroup = CollisionFilterGroups.CustomFilter1;

            var behaviour = ball.GetOrCreate<BallBehaviour>();
            behaviour.direction = direction;

            ball.Transform.Position = Entity.Transform.Position;
            Entity.Scene.Entities.Add(ball);

            shootBallInstance.Play();
        }
    }
}
