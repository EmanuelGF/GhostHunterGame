using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;
using Stride.Rendering.Sprites;

namespace RpgGame
{
    public class GhostHunterControls : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private int speed = 3;
        private float accumulatedTime = 0;
        private float frameDuration = 0.1f;
        private SpriteFromSheet sprite;
        public Direction direction;
        private RigidbodyComponent physics;
        private Vector3 lastOkPosition;

        public override void Start()
        {
            // Initialization of the script.
            sprite = Entity.Get<SpriteComponent>().SpriteProvider as SpriteFromSheet;
            sprite.CurrentFrame = 0;

            physics = Entity.Get<RigidbodyComponent>();
            direction = Direction.Down;
        }

        public override void Update()
        {
            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

            HandleCollisions();

            if (Input.HasKeyboard)
            {
                HandleMovement(deltaTime);
            }
        }

        private void HandleCollisions()
        {
            if (physics.Collisions.Count <= 0)
                lastOkPosition = Entity.Transform.Position;

            if (physics.Collisions.Count > 0 && direction == Direction.Up)
                Entity.Transform.Position = lastOkPosition - new Vector3(0, 0.1F, 0);
            if (physics.Collisions.Count > 0 && direction == Direction.Down)
                Entity.Transform.Position = lastOkPosition - new Vector3(0, -0.1F, 0);
            if (physics.Collisions.Count > 0 && direction == Direction.Left)
                Entity.Transform.Position = lastOkPosition - new Vector3(-0.1F, 0, 0);
            if (physics.Collisions.Count > 0 && direction == Direction.Right)
                Entity.Transform.Position = lastOkPosition - new Vector3(0.1F, 0, 0);
        }

        private void HandleMovement(float deltaTime)
        {
            bool animationStarted = false;

            if (Input.IsKeyDown(Keys.W))
            {
                direction = Direction.Up;
                Entity.Transform.Position.Y += speed * deltaTime;
                StartAnimation(startFrame: 13, endFrame: 16, deltaTime);
                animationStarted = true;
            }
            else if (Input.IsKeyDown(Keys.S))
            {
                direction = Direction.Down;
                Entity.Transform.Position.Y -= speed * deltaTime;
                StartAnimation(startFrame: 1, endFrame: 4, deltaTime);
                animationStarted = true;
            }
            else if (Input.IsKeyDown(Keys.A))
            {
                direction = Direction.Left;
                Entity.Transform.Position.X -= speed * deltaTime;
                StartAnimation(startFrame: 9, endFrame: 12, deltaTime);
                animationStarted = true;
            }
            else if (Input.IsKeyDown(Keys.D))
            {
                direction = Direction.Right;
                Entity.Transform.Position.X += speed * deltaTime;
                StartAnimation(startFrame: 5, endFrame: 8, deltaTime);
                animationStarted = true;
            }

            if (!animationStarted)
            {
                // Reset the accumulated time if no animation is started.
                accumulatedTime = 0;
            }
        }

        private void StartAnimation(int startFrame, int endFrame, float deltaTime)
        {
            accumulatedTime += deltaTime;

            if (accumulatedTime >= frameDuration)
            {
                sprite.CurrentFrame += 1;
                if (sprite.CurrentFrame > endFrame || sprite.CurrentFrame < startFrame)
                {
                    sprite.CurrentFrame = startFrame;
                }
                accumulatedTime -= frameDuration;
            }
        }
    }
}
