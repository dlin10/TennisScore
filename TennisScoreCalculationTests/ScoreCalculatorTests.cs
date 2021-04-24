using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Serialization;
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

        [DataTestMethod]
        [DynamicData(nameof(TestPointNames_GetData),DynamicDataSourceType.Method)]
        public void TestPointNames(int pointsCount, string expected)
        {
            WinPoints(true, pointsCount);
            var score = _scoreCalculator.GetCurrentScore();
            var actual = score.PlayerXScore.Points;
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> TestPointNames_GetData()
        {
            yield return new object[] {0, "0"};
            yield return new object[] { 1, "15" };
            yield return new object[] { 2, "30" };
            yield return new object[] { 3, "40" };
        }

        private void WinPoints(bool playerX, int count)
        {
            foreach (var b in Enumerable.Repeat(playerX, count))
            {
                _scoreCalculator.Increment(b);
            }
        }

        private void WinGameForPlayer(bool playerX)
        {
            WinPoints(playerX, 4);
        }

        private void CreateDeuce()
        {
            WinPoints(true, 3);
            WinPoints(false, 3);
        }
    }
}
