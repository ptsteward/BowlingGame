using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    class Program
    {
        private static int currentFrame = 1;
        private static int currentRoll = 1;
        private static int frameLimit = 10;
        private static ICollection<int> frame10 = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Would you like to play a game? y/n");
            var entry = Console.ReadLine();
            if (entry.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                var game = new GameEngine();
                while (currentFrame <= frameLimit)
                {                   

                    Console.WriteLine($"Frame {currentFrame}: Roll {currentRoll}:");
                    var roll = GetRollEntry(game);
                    HandleFrame10(roll);
                    AdvanceCurrentRollAndFrame(roll);

                }
                EndGame(game);
            }
        }

        private static int GetRollEntry(GameEngine game)
        {
            var isRollValid = false;
            while (!isRollValid)
            {
                Console.WriteLine("Pins Knocked Down:");
                var roll = Console.ReadLine();
                var isRollNumber = Int32.TryParse(roll, out var rollValue);
                if (isRollNumber && rollValue <= 10)
                {
                    isRollValid = true;
                    game.Roll(rollValue);
                    Console.WriteLine($"Current Score: {game.CalculateScore()}");
                    return rollValue;                    
                }
                else
                {
                    Console.WriteLine("Invalid Entry");
                }
            }
            return 0;
        }

        private static void EndGame(GameEngine game)
        {
            Console.WriteLine("********************");
            Console.WriteLine("Game Over");
            Console.WriteLine($"Final Score: {game.CalculateScore()}");
            Console.WriteLine("********************");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void AdvanceCurrentRollAndFrame(int roll)
        {
            if (roll == 10)
            {
                currentFrame++;
                currentRoll = 1;
            }
            else if (currentRoll == 2)
            {
                currentRoll = 1;
                currentFrame++;
            }
            else
            {
                currentRoll++;
            }
        }

        private static void HandleFrame10(int roll)
        {
            if (currentFrame == 10)
            {
                frame10.Add(roll);
                CalculateExtraFrames();
            }
        }

        private static void CalculateExtraFrames()
        {
            if (frame10.Contains(10))
                frameLimit += 2;
            else if (frame10.Sum() == 10)
                frameLimit++;
        }
    }
}
