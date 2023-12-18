using RpgGame.Services;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace RpgGame
{
    public class ShowInGameScore : StartupScript
    {
        public override void Start()
        {
            ScoreManager.OnScoreChanged += UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int newScore)
        {
            var page = Entity.Get<UIComponent>().Page;
            var scoreText = page.RootElement.FindVisualChildOfType<TextBlock>("InGameScore");
            scoreText.Text = $"Score: {newScore}";
        }
    }
}
