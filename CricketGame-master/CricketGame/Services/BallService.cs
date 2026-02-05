using CricketGame.Config;
using CricketGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketGame.Services;
using System.Threading;
using System.Runtime.InteropServices;

namespace CricketGame.Services
{
    internal class BallService
    {
        private Random random = new Random();
        ScoreboardService overSummary = new ScoreboardService();


        public void BowlBall(Innings innings)
        {
            if (innings.IsCompleted) return;

            innings.isOverInitiated = true;
            bool isNoBall = random.Next(0, 10) == 0;


        // NO BALL LOGIC
        nb:
            if (isNoBall)
            {
                int batRuns = random.Next(0, 7); // 0–6 runs
                int totalRuns = 1 + batRuns;     // 1 NB + bat runs
                innings.CurrentBowler.RunsConceded += totalRuns;
                innings.CurrentBowler.BallDelivered++;
                innings.CurrentBowler.Extras++;

                innings.TotalRuns += totalRuns;
                innings.Striker.Runs += batRuns;

                bool isNoBallAgain = random.Next(0, 10) == 0;


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(
                    $"No Ball! +1 | {innings.Striker.Name} scores {batRuns}");
                Console.ResetColor();

                if (isNoBallAgain) { goto nb; }
                // Strike rotation on odd bat runs
                if (batRuns % 2 == 1)
                {
                    innings.SwapStrike();
                }

                // Target check (2nd innings)
                if (innings.Target > 0 && innings.TotalRuns >= innings.Target)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                        $"\nTarget achieved! {innings.BattingTeam.Name} wins!");
                    Console.WriteLine($"{innings.BattingTeam.Name} won by {GameConfig.PlayersPerTeam - innings.Wickets}");
                    Console.ResetColor();
                    innings.IsCompleted = true;
                }


                return;
            }

            int[] outcomes = { 0, 1, 2, 3, 4, 6, -1, -2 };

            int outcome = outcomes[random.Next(outcomes.Length)];


            

            //TARGET NOT ACHIEVED
            //if (innings.OversCompleted == GameConfig.OversPerInnings && innings.TotalRuns < innings.Target)
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine($"\n{innings.BowlingTeam.Name} won the match! by{innings.Target - innings.TotalRuns}");
            //    Console.ResetColor();
            //    innings.IsCompleted = true;
            //    return;
            //}
            //Wide ball
            if (outcome == -2)
            {
                innings.TotalRuns += 1;
                innings.CurrentBowler.RunsConceded += 1;
                innings.CurrentBowler.BallDelivered++;
                innings.CurrentBowler.Extras++;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{innings.CurrentBowler.Name} bowls to {innings.Striker.Name}");
                Console.WriteLine("\tWide Ball! +1 run");
                Console.ResetColor();
                return;
            }


            innings.BallsInCurrentOver++;
            innings.Striker.BallsFaced++;
            //Wicket
            if (outcome == -1)
            {
                innings.Wickets++;
                innings.Striker.IsOut = true;
                innings.CurrentBowler.Wickets++;
                innings.CurrentBowler.BallDelivered++;
                Console.Write($"{innings.CurrentBowler.Name} bowls to {innings.Striker.Name}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tWICKET! {innings.Striker.Name} is OUT!!");
                Console.ResetColor();
                innings.NextBatsman();
            }
            else
            {
                innings.TotalRuns += outcome;
                innings.Striker.Runs += outcome;
                innings.CurrentBowler.RunsConceded += outcome;
                innings.CurrentBowler.BallDelivered++;

                Console.Write($"{innings.CurrentBowler.Name} bowls to {innings.Striker.Name}");
                Console.WriteLine($"\t{innings.Striker.Name} Scores: {outcome}");

                if (outcome == 1 || outcome == 3)
                    innings.SwapStrike();
            }

            if (innings.BallsInCurrentOver == GameConfig.BallsPerOver)
            {
                innings.OversCompleted++;
                innings.BallsInCurrentOver = 0;
                innings.isOverInitiated = false;
                SwapStrike(innings);
                overSummary.ShowOverSummary(innings);
                innings.PreviousBowler = innings.CurrentBowler;

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
