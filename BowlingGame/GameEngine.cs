using System;
using System.Linq;

namespace BowlingGame
{
    public class GameEngine
    {
        private int[] rolls = new int[21];
        private int currentRoll = 0;

        public GameEngine Roll(int pins)
        {
            rolls[currentRoll++] = pins;
            return this;
        }

        public int CalculateScore()
        {
            int rollIndex = 0;
            var score = Enumerable.Range(0, 10).Aggregate(0, (runningScore, frame) =>
            {
                if (IsFrameAStrike(rollIndex))
                {
                    runningScore = CalculateScoreForStrike(runningScore, rollIndex);
                    rollIndex++;
                }
                else if (IsFrameASpare(rollIndex))
                {
                    runningScore = CalculateScoreForSpare(runningScore, rollIndex);
                    rollIndex += 2;
                }
                else
                {
                    runningScore = CalculateScoreForFrame(runningScore, rollIndex);
                    rollIndex += 2;
                }
                return runningScore;
            });
            return score;
        }

        private bool IsFrameASpare(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1] == 10;

        private bool IsFrameAStrike(int rollIndex) => rolls[rollIndex] == 10;

        private int CalculateScoreForStrike(int score, int rollIndex) => score += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2];

        private int CalculateScoreForSpare(int score, int rollIndex) => score += 10 + rolls[rollIndex + 2];

        private int CalculateScoreForFrame(int score, int rollIndex) => score += rolls[rollIndex] + rolls[rollIndex + 1];
    }
}
