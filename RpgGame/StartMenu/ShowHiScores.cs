using RpgGame.Services;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.StartMenu
{
    public class ShowHiScores : AsyncScript
    {
        public override async Task Execute()
        {
            var apiService = new ApiService();
            try
            {
                // Get the HiScores UI page.
                var page = Entity.Get<UIComponent>().Page;

                // Get the HiScores UI element.
                var hiScores = page.RootElement.FindVisualChildOfType<TextBlock>("HiScoresList");

                // Get the HiScores from the API.
                var scores = await apiService.GetTopTenScoresAsync();

                // Display the HiScores.
                var sb = new StringBuilder();
                foreach (var score in scores)
                {
                    sb.AppendLine($"{score.PlayerName} - {score.PlayerScore}");
                }
                hiScores.Text = sb.ToString();

            }
            catch (Exception)
            { }
        }
    }
}
