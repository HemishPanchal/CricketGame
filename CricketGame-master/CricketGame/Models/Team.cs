using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CricketGame.Models
{
    internal class Team
    {
        public string Name;

        // Players in batting order
        public List<Player> Players;

        // Derived list: only bowlers
        public List<Player> Bowlers
        {
            get
            {
                return Players.Where(p => p.IsBowler).ToList();
            }
        }
    }
}
