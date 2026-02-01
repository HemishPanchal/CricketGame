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
        public Team BattingTeam;
        public Team BowlingTeam;
        public Player Striker;
        public Player NonStriker;
        public Player CurrentBowler;

        public int TotalRuns;
        public int Wickets;
        public int OversCompleted;
        public int BallsInOver;
        

    }
}
