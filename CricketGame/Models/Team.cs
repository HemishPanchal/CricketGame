using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketGame.Models
{
    internal class Team
    {
        public string TeamName;

        List<Player> Players = new List<Player>();

        public int TotalRuns;
        public int Wickets;
    }
}
