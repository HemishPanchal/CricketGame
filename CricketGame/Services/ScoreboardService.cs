using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketGame.Services;


namespace CricketGame.Services
{
    internal class ScoreboardService
    {
        public void ShowOverSummary(Innings innings)
        {
            Console.WriteLine(
                $"Over {innings.OversCompleted}: " +
                $"{innings.TotalRuns}/{innings.Wickets}"
            );
            Console.WriteLine("--------------------------------");
        }

        public void ShowInningsSummary(Innings innings)
        {
            Console.WriteLine("\nINNINGS COMPLETE");
            Console.WriteLine($"Score: {innings.TotalRuns}/{innings.Wickets}");
            Console.WriteLine($"Overs: {innings.OversCompleted}");
            Console.WriteLine("--------------------------------");

            foreach (var p in innings.BattingOrder)
            {
                Console.WriteLine(
                    $"{p.Name} - {p.Runs} ({p.BallsFaced}) " +
                    $"{(p.IsOut ? "OUT" : "NOT OUT")}"
                );
            }
        }
    }
}

