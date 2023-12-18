using RpgGame.Services;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RpgGame.SaveScore
{
    public class ConfirmButtonClick : AsyncScript
    {
        private UIPage page;
        public override async Task Execute()
        {
            page = Entity.Get<UIComponent>().Page;
            var confirmButton = page.RootElement.FindVisualChildOfType<Button>("ConfirmButton");

            confirmButton.Click += async (sender, args) =>
            {
                page = Entity.Get<UIComponent>().Page;
                var playerName = page.RootElement.FindVisualChildOfType<EditText>("PlayerName");

                var apiService = new ApiService();
                try
                {
                    await apiService.SubmitScoreAsync(playerName.Text, ScoreManager.Score);
                }
                catch (Exception)
                {
                    // TODO: Handle exception.
                }

                // Unload the SaveScore child scene.
                Content.Unload(SceneSystem.SceneInstance.RootScene.Children
                    .First(s => s.Name == "SaveScore"));

                // Remove the SaveScore scene from the current scene.
                SceneSystem.SceneInstance.RootScene.Children.Remove(Entity.Scene);

                // Reset the current scene.
                Content.Unload(SceneSystem.SceneInstance.RootScene);

                // Load the StartMenu scene.
                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("Scenes/StartMenu");
            };
        }
    }
}
