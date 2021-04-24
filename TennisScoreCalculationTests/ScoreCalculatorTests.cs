using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TennisScoreCalculation;

namespace TennisScoreCalculationTests
{
    [TestClass]
    public class ScoreCalculatorTests
    {
        private ScoreCalculator _scoreCalculator;

        [TestInitialize]
        public void Setup()
        {
            _scoreCalculator = new ScoreCalculator("Player X", "Player Y");
        }

        [TestMethod]
        public void MatchNotStartedTest()
        {
            var score = _scoreCalculator.GetCurrentScore();

            Assert.IsNotNull(score);
            Assert.IsNotNull(score.PlayerXScore);
            Assert.IsNotNull(score.PlayerYScore);
            Assert.AreEqual("0", score.PlayerXScore.Points);
            Assert.AreEqual("0", score.PlayerYScore.Points);
            Assert.AreEqual(0,score.PlayerXScore.Sets.Length);
            Assert.AreEqual(0, score.PlayerYScore.Sets.Length);
        }

        [TestMethod]
        public void EventRaisedOnPointTest()
        {
            Score scoreFromEvent = null;
            _scoreCalculator.ScoreChanged += score => scoreFromEvent = score;
            _scoreCalculator.Increment(true);
            Assert.IsNotNull(scoreFromEvent);
        }

        [DataTestMethod]
        [DynamicData(nameof(TestPointNames_GetData), DynamicDataSourceType.Method)]
        public void TestPointNames(int pointsCount, string expected)
        {
            WinPointsForPlayer(true, pointsCount);
            var score = _scoreCalculator.GetCurrentScore();
            var actual = score.PlayerXScore.Points;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AdvantagePointNamesTest()
        {
            CreateDeuce();

            _scoreCalculator.Increment(true);
            var score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual("Ad", score.PlayerXScore.Points);
            Assert.AreEqual("40", score.PlayerYScore.Points);

            _scoreCalculator.Increment(false);
            score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual("40", score.PlayerXScore.Points);
            Assert.AreEqual("40", score.PlayerYScore.Points);

            _scoreCalculator.Increment(false);
            score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual("40", score.PlayerXScore.Points);
            Assert.AreEqual("Ad", score.PlayerYScore.Points);
        }

        [TestMethod]
        public void SetIncrementedTest()
        {
            WinGameForPlayer(true);
            var score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual("0", score.PlayerXScore.Points);
            Assert.AreEqual(1, score.PlayerXScore.Sets.Length);
            Assert.AreEqual(1, score.PlayerYScore.Sets.Length);
            Assert.AreEqual(1, score.PlayerXScore.Sets[0]);
            Assert.AreEqual(0, score.PlayerYScore.Sets[0]);
        }

        [TestMethod]
        public void MatchIncrementedTest()
        {
            WinSetForPlayer(true);
            var score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual("0", score.PlayerXScore.Points);
            Assert.AreEqual(1, score.PlayerXScore.Sets.Length);
            Assert.AreEqual(1, score.PlayerYScore.Sets.Length);
            Assert.AreEqual(6, score.PlayerXScore.Sets[0]);
            Assert.AreEqual(0, score.PlayerYScore.Sets[0]);

            _scoreCalculator.Increment(true);
            score = _scoreCalculator.GetCurrentScore();
            Assert.AreEqual(2, score.PlayerXScore.Sets.Length);
            Assert.AreEqual(2, score.PlayerYScore.Sets.Length);
            Assert.AreEqual(0, score.PlayerXScore.Sets[1]);
            Assert.AreEqual(0, score.PlayerYScore.Sets[1]);
        }

        private static IEnumerable<object[]> TestPointNames_GetData()
        {
            yield return new object[] {0, "0"};
            yield return new object[] { 1, "15" };
            yield return new object[] { 2, "30" };
            yield return new object[] { 3, "40" };
        }

        private void WinPointsForPlayer(bool playerX, int pointsCount)
        {
            for (int i = 0; i < pointsCount; i++)
            {
                _scoreCalculator.Increment(playerX);
            }
        }

        private void WinGameForPlayer(bool playerX)
        {
            WinPointsForPlayer(playerX, 4);
        }

        private void CreateDeuce()
        {
            WinPointsForPlayer(true, 3);
            WinPointsForPlayer(false, 3);
        }

        private void WinGamesForPlayer(bool playerX, int count)
        {
            for (int i = 0; i < count; i++)
            {
                WinGameForPlayer(playerX);
            }
        }

        private void WinSetForPlayer(bool playerX)
        {
            WinGamesForPlayer(playerX, 6);
        }
    }
}
