using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketGame.Models;

namespace CricketGame.Services
{
    internal class Innings
    {
        // Teams
        public Team BattingTeam;
        public Team BowlingTeam;

        // Batting
        public List<Player> BattingOrder;
        public Player Striker;
        public Player NonStriker;
        public int NextBatsmanIndex = 2;

        // Bowling
        public List<Player> Bowlers;
        public Player CurrentBowler;
        public Player PreviousBowler;
        

        // Score state
        public int TotalRuns;
        public int Wickets;
        public int OversCompleted;
        public int BallsInCurrentOver;
        public bool isOverInitiated;

        // Match control
        public bool IsCompleted;
        public int Target;


        public Innings(Team battingTeam, Team bowlingTeam)
        {
            BattingTeam = battingTeam;
            BowlingTeam = bowlingTeam;

            BattingOrder = battingTeam.Players;
            Bowlers = bowlingTeam.Bowlers;

            Striker = BattingOrder[0];
            NonStriker = BattingOrder[1];

            CurrentBowler = Bowlers[0];
        }

        // Called when wicket falls
        public void NextBatsman()
        {
            if (NextBatsmanIndex < BattingOrder.Count)
            {
                Striker = BattingOrder[NextBatsmanIndex];
                NextBatsmanIndex++;
            }
            else
            {
                IsCompleted = true;
            }
        }

        // Strike rotation
        public void SwapStrike()
        {
            var temp = Striker;
            Striker = NonStriker;
            NonStriker = temp;
        }

        // Change bowler after over
        public void SelectBowler()
        {
            List<Player> availableBowlers = Bowlers.Where(b => b != PreviousBowler).ToList();
            Console.WriteLine("\n Choose Bowler: ");

            for (int i = 0; i < availableBowlers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableBowlers[i].Name}");
            }
            int choice;
            while (true)
            {
                Console.WriteLine("Enter Choice: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= availableBowlers.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid Choice. Try Again.");
            }
            CurrentBowler = availableBowlers[choice - 1];
        }


        
    }
}

