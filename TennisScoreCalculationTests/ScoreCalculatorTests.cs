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
    }
}
