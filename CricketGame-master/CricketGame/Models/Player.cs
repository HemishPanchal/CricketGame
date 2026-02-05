using System;
namespace CricketGame.Models 
{ 
public class Player
{
		public string Name;
		public string Role;
		public bool IsOut;
		public bool IsBowler;
		public int Runs;
        public int RunsConceded;
        public int BallsFaced;
        public int BallDelivered;
		public int Wickets;
        public int Extras;


        public void ResetForNewMatch()
        {
            IsOut = false;
            Runs = 0;
            RunsConceded = 0;
            BallsFaced = 0;
            BallDelivered = 0;
            Wickets = 0;
            Extras = 0;
        }

    }
}
