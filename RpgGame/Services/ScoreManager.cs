using System;

namespace RpgGame.Services
{
    public static class ScoreManager
    {
        public static int Score { get; set; } = 0;

        public static event Action<int> OnScoreChanged;

        public static void AddScore(int points)
        {
            Score += points;
            OnScoreChanged?.Invoke(Score);
        }

        public static void ResetScore()
        {
            Score = 0;
        }
    }
}
