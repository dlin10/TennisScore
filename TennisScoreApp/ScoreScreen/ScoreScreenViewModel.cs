using TennisScoreCalculation;

namespace TennisScoreApp.ScoreScreen
{
    public class ScoreScreenViewModel  :NotificationObject
    {
        private Score _score;

        public ScoreScreenViewModel(ScoreCalculator scoreCalculator)
        {
            scoreCalculator.ScoreChanged += score=> Score = score;
            Score = scoreCalculator.GetCurrentScore();
        }

        public Score Score
        {
            get => _score;
            set
            {
                _score = value;
                RaisePropertyChanged(nameof(Score));
            }
        }
    }
}
