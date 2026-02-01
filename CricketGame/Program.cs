using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketGame.Models;
using CricketGame.Services;
using CricketGame.Config;

namespace CricketGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Team india = new Team { TeamName = "India" };
            Team pakistan = new Team { TeamName = "Pakistan" };
            Team aus = new Team { TeamName = "Australia" };
        }
    }
}
