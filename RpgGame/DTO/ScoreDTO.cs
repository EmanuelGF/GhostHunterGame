using System.Text.Json.Serialization;

namespace RpgGame.DTO
{
    public class ScoreDTO
    {
        [JsonPropertyName("playerName")]
        public string PlayerName { get; set; }

        [JsonPropertyName("playerScore")]
        public int PlayerScore { get; set; }
    }
}
