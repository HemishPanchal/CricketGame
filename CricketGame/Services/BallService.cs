using CricketGame.Config;
using CricketGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketGame.Services;

namespace CricketGame.Services
{
    internal class BallService
    {
        private Random random = new Random();

        public void BowlBall(Innings innings)
        {
            if (innings.IsCompleted) return;

            int[] outcomes = { 0, 1, 2, 4, 6, -1, -2 };
            int outcome = outcomes[random.Next(outcomes.Length)];

            // NO BALL
            if (outcome == -2)
            {
                innings.TotalRuns += 1;
                Console.WriteLine("No Ball! +1 run");
                return;
            }

            innings.BallsInCurrentOver++;
            innings.Striker.BallsFaced++;

            if (outcome == -1)
            {
                innings.Wickets++;
                innings.Striker.IsOut = true;
                Console.WriteLine("WICKET!");
                innings.NextBatsman();
            }
            else
            {
                innings.TotalRuns += outcome;
                innings.Striker.Runs += outcome;

                Console.WriteLine($"Runs: {outcome}");

                if (outcome == 1)
                    innings.SwapStrike();
            }

            if (innings.BallsInCurrentOver == GameConfig.BallsPerOver)
            {
                innings.OversCompleted++;
                innings.BallsInCurrentOver = 0;

                SwapStrike(innings);
                innings.ChangeBowler();
            }

            if (innings.OversCompleted == GameConfig.OversPerInnings)
            {
                innings.IsCompleted = true;
            }
        }
        private void SwapStrike(Innings innings)
        {
            Player temp = innings.Striker;
            innings.Striker = innings.NonStriker;
            innings.NonStriker = temp;
        }



    }
}
