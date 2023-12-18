using RpgGame.Services;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace RpgGame.SaveScore
{
    public class ShowScore : StartupScript
    {
        public override void Start()
        {
            var page = Entity.Get<UIComponent>().Page;

            var scoreTextBlock = page.RootElement.FindVisualChildOfType<TextBlock>("Score");
            scoreTextBlock.Text = $"{ScoreManager.Score} points!!";
        }
    }
}
