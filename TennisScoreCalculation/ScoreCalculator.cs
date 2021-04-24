using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TennisScoreCalculation
{
    public class ScoreCalculator
    {
        private readonly string _playerX;
        private readonly string _playerY;
        private Point _currentGame;
        private readonly List<Point> _sets;

        public ScoreCalculator(string playerX, string playerY)
        {
            _playerX = playerX;
            _playerY = playerY;
            _sets = new List<Point>{new Point()};
        }

        public event Action<Score> ScoreChanged;

        public void Increment(bool playerXGotPoint)
        {
            if (IncrementPoints(playerXGotPoint))
            {
                _currentGame = new Point();
                if (IncrementGame(playerXGotPoint))
                {
                    _sets.Add(new Point());
                }
            }

            ScoreChanged?.Invoke(GetCurrentScore());
        }


        public Score GetCurrentScore()
        {
            var score = new Score();

            score.PlayerXScore.PlayerName = _playerX;
            score.PlayerXScore.Sets = _sets.Select(s => s.X).ToArray();

            score.PlayerYScore.PlayerName = _playerY;
            score.PlayerYScore.Sets = _sets.Select(s => s.Y).ToArray();

            DefineGamePoints(out var playerXPoints, out var playerYPoints);
            score.PlayerXScore.Points = playerXPoints;
            score.PlayerYScore.Points = playerYPoints;

            return score;
        }

        private void DefineGamePoints(out string playerXPoints, out string playerYPoints)
        {
            if (_currentGame.X + _currentGame.Y >= 6)
            {
                //Deuce mode
                if (_currentGame.X == _currentGame.Y)
                {
                    playerXPoints = playerYPoints = "40";
                    return;
                }

                if (_currentGame.X > _currentGame.Y)
                {
                    playerXPoints = "Ad";
                    playerYPoints = "40";
                }
                else
                {
                    playerXPoints = "40";
                    playerYPoints = "Ad";
                }

                return;
            }

            playerXPoints = GetPoints(_currentGame.X);
            playerYPoints = GetPoints(_currentGame.Y);

            static string GetPoints(int i)
            {
                return i switch
                {
                    0 => "0",
                    1 => "15",
                    2 => "30",
                    3 => "40",
                    _ => throw new Exception("Should be in deuce mode")
                };
            }
        }

        /// <summary>
        /// Increments point and defines if game was finished
        /// </summary>
        /// <param name="playerXGotPoint"></param>
        /// <returns>True if game is finished</returns>
        private bool IncrementPoints(bool playerXGotPoint)
        {
            if (playerXGotPoint)
            {
                _currentGame.Offset(1, 0);
            }
            else
            {
                _currentGame.Offset(0, 1);
            }

            if (_currentGame.X + _currentGame.Y > 6)
            {
                //Deuce mode
                return Math.Abs(_currentGame.X - _currentGame.Y) >= 2;
            }

            //Regular mode
            return _currentGame.X > 3 || _currentGame.Y > 3;
        }

        /// <summary>
        /// Increments game and defines if set was finished
        /// </summary>
        /// <param name="playerXGotPoint"></param>
        /// <returns></returns>
        private bool IncrementGame(bool playerXGotPoint)
        {
            var currentSet = _sets[^1]; //^1 - index of last item
            if (playerXGotPoint)
            {
                currentSet.Offset(1,0);
            }
            else
            {
                currentSet.Offset(0,1);
            }
            _sets[^1] = currentSet; 

            return IsSetFinished(currentSet.X, currentSet.Y) || IsSetFinished(currentSet.Y, currentSet.X);
        }

        private static bool IsSetFinished(int a, int b)
        {
            if (a == 6 && b <= 4)
            {
                return true;
            }

            return a == 7;
        }
    }
}
