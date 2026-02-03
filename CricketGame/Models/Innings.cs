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
        private int CurrentBowlerIndex = 0;

        // Score state
        public int TotalRuns;
        public int Wickets;
        public int OversCompleted;
        public int BallsInCurrentOver;

        // Match control
        public bool IsCompleted;
        public int Target;

        // ✅ Constructor (THIS WAS MISSING)
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
        public void ChangeBowler()
        {
            CurrentBowlerIndex++;

            if (CurrentBowlerIndex >= Bowlers.Count)
                CurrentBowlerIndex = 0;

            CurrentBowler = Bowlers[CurrentBowlerIndex];
        }
    }
}

