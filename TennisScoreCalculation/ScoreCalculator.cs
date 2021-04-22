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
        private List<Point> _sets;

        public ScoreCalculator(string playerX, string playerY)
        {
            _playerX = playerX;
            _playerY = playerY;
            _sets = new List<Point>();
        }

        public void Increment(bool playerXGotPoint)
        {
            throw new NotImplementedException();

        }

        public Score GetCurrentScore()
        {
            throw new NotImplementedException();
        }
    }
}
