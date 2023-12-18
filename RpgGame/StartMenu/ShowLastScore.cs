using RpgGame.Services;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace RpgGame.StartMenu
{
    public class ShowLastScore : StartupScript
    {
        public override void Start()
        {
            // Show latest score on the start menu.
            var page = Entity.Get<UIComponent>().Page;
            var scoreText = page.RootElement.FindVisualChildOfType<TextBlock>("Score");
            scoreText.Text = ScoreManager.Score.ToString();
        }
    }
}
