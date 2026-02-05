using System;
namespace CricketGame.Services {
public class TossService
{
	private Random rnd = new Random();
	public int DoToss() {
		return rnd.Next(0,2);
	}
}
}
