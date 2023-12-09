using Stride.Core.Mathematics;
using Stride.Engine;

namespace RpgGame
{
    public class CameraBehaviour : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public Entity EntityToFollow;
        private readonly int followSpeed = 3;


        public override void Update()
        {
            // Do stuff every new frame
            var deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
            var currentPosition = Entity.Transform.Position;
            var otherPosition = EntityToFollow.Transform.Position;
            var x = MathUtil.Lerp(currentPosition.X, otherPosition.X, deltaTime * followSpeed);
            var y = MathUtil.Lerp(currentPosition.Y, otherPosition.Y, deltaTime * followSpeed);
            var z = 50;
            Entity.Transform.Position = new Vector3(x, y, z);
        }
    }
}
