using BowlingGame;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class GameEngineTests
    {
        private GameEngine game;

        [SetUp]
        public void SetUp()
        {
            game = new GameEngine();
        }

        [Test]
        public void Roll_All_Gutter_Balls()
        {
            RollMultipleFrames(20, 0);

            Assert.AreEqual(0, game.CalculateScore());
        }

        [Test]
        public void Roll_All_Single_Pins()
        {
            RollMultipleFrames(20, 1);

            Assert.AreEqual(20, game.CalculateScore());
        }

        [Test]
        public void Roll_A_Single_Spare()
        {
            RollSpare();
            game.Roll(5);
            RollMultipleFrames(17, 0);

            Assert.AreEqual(20, game.CalculateScore());
        }

        [Test]
        public void Roll_A_Single_Strike()
        {
            RollStrike();
            game.Roll(3).Roll(4);
            RollMultipleFrames(16, 0);

            Assert.AreEqual(24, game.CalculateScore());
        }

        [Test]
        public void Roll_All_Strikes()
        {
            RollMultipleFrames(12, 10);

            Assert.AreEqual(300, game.CalculateScore());
        }

        private void RollMultipleFrames(int rolls, int pins) 
            => Enumerable.Range(0, rolls).ToList().ForEach(roll => game.Roll(pins));

        private void RollSpare() => game.Roll(7).Roll(3);

        private void RollStrike() => game.Roll(10);
    }
}