using RpgGame.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RpgGame.Services
{
    public class ApiService
    {
        private static readonly Lazy<ApiService> _instance = new(() => new ApiService());  
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        
        public static ApiService Instance => _instance.Value;

        private ApiService()
        {
            _httpClient = new HttpClient();
            var configuration = ConfigurationReader.GetConfiguration();
            _apiBaseUrl = configuration["ApiBaseUrl"];
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
