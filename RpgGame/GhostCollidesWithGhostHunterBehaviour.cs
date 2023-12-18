using RpgGame.Services;
using Stride.Engine;
using Stride.Physics;
using System.Threading.Tasks;

namespace RpgGame
{
    public class GhostCollidesWithGhostHunterBehaviour : AsyncScript
    {
        private Scene saveScore;

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

            if (affected.Name == "GhostHunter")
            {
                if (ScoreManager.Score > 0)
                {
                    // Remove the GhostHunter from the scene.
                    affected.Scene = null;
                    affected.Dispose();

                    // Load the SaveScore child scene.
                    saveScore = Content.Load<Scene>("Scenes/SaveScore");
                    saveScore.Name = "SaveScore";

                    // Add the SaveScore scene to the current scene.
                    SceneSystem.SceneInstance.RootScene.Children.Add(saveScore);

                }
                else
                {
                    // Reset the current scene.
                    Content.Unload(SceneSystem.SceneInstance.RootScene);

                    // Load the StartMenu scene.
                    SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("Scenes/StartMenu");
                }
            }
        }
    }
}
