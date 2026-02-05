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
            Console.WriteLine("===================================");
            
            
            Console.WriteLine(
                $"Over {innings.OversCompleted}: " +
                $" Score {innings.TotalRuns}/{innings.Wickets}"
            );
            Console.WriteLine("===================================");

        }

        public void ShowInningsSummary(Innings innings)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("\nINNINGS COMPLETE");
            Console.WriteLine($"Score: {innings.TotalRuns}/{innings.Wickets}");
            Console.WriteLine($"Overs: {innings.OversCompleted}");

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("BATTING SUMMARY");
            Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} ","Name","Runs","Ball Faced","Status");
            foreach (var p in innings.BattingOrder)
            {
                Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} ", p.Name, p.Runs, p.BallsFaced, (p.IsOut ? "OUT" : "NOT OUT"));
            }
            Console.WriteLine("-----------------------------------");
            
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("BOWLING SUMMARY");
            Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-25} {4,-25} ", "Name", "Wickets", "Runs Conceded", "Ball Delivered","Extras");
            foreach (var p in innings.Bowlers)
            {
                Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-25} {4,-25}", p.Name, p.Wickets,p.RunsConceded,p.BallDelivered,p.Extras);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("===================================");
        }
    }
}

