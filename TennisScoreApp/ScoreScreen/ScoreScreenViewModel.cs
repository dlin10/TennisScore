using System;
using System.Collections.Generic;
using System.Text;
using TennisScoreCalculation;

namespace TennisScoreApp.ScoreScreen
{
    public class ScoreScreenViewModel  :NotificationObject
    {
        private Score _score;

        public ScoreScreenViewModel(ScoreCalculator scoreCalculator)
        {
            scoreCalculator.ScoreChanged += score=>
                                            {
                                                _score = score;
                                                RaisePropertyChanged();
                                            };
        }
    }
}
