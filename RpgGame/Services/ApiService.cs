using RpgGame.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RpgGame.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "https://localhost:7165/api/Scores";
        }

        public async Task<bool> SubmitScoreAsync(string playerName, int score)
        {
            var payload = new ScoreDTO { PlayerName = playerName, PlayerScore = score };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/PostScore", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<ScoreDTO[]> GetTopTenScoresAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetTopTenScores");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ScoreDTO[]>(json);
            }

            return null;
        }
    }
}
