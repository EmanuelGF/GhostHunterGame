using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace RpgGame.StartMenu
{
    public class PlayButtonClick : StartupScript
    {

        public override void Start()
        {
            var page = Entity.Get<UIComponent>().Page;

            var button = page.RootElement.FindVisualChildOfType<Button>("PlayButton");
            button.Click += (sender, args) =>
            {
                ScoreManager.ResetScore();

                // Reset the current scene.
                Content.Unload(SceneSystem.SceneInstance.RootScene);

                // Load the LevelOne scene.
                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("Scenes/LevelOne");

            };
        }
    }
}
