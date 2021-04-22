namespace TennisScoreCalculation
{
    public class Score
    {
        public Score()
        {
            PlayerXScore = new PlayerScore();
            PlayerYScore = new PlayerScore();
        }

        public PlayerScore PlayerXScore { get; }

        public PlayerScore PlayerYScore { get; }
    }
}