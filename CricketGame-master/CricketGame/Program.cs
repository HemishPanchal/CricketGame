using CricketGame.Config;
using CricketGame.Models;
using CricketGame.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CricketGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TossService tossService = new TossService();
            void GetTeamInfo(Team t1, Team t2) {
                Console.WriteLine(t1.Name);
                Console.WriteLine("{0,-25} {1,-10}","Name","Role");
                foreach (var i in t1.Players) {
                    Console.WriteLine("{0,-25} {1,-10}",i.Name,i.Role);
                }
                Console.WriteLine();
                Console.WriteLine(t2.Name);
                Console.WriteLine("{0,-25} {1,-10}", "Name", "Role");
                
                foreach (var i in t2.Players)
                {
                    Console.WriteLine("{0,-25} {1,-10}", i.Name, i.Role);
                }
            }
            //TEAMS
            Team india = new Team
            {
                Name = "India",
                Players = new List<Player>
                {
                    new Player { Name = "Rohit", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Kohli", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Surya", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Pant", Role = "WK-Batsman", IsBowler = false },
                    new Player { Name = "Jadeja", Role = "All-rounder", IsBowler = true },
                    new Player { Name = "Shami", Role = "Bowler", IsBowler = true },
                    new Player { Name = "Bumrah", Role = "Bowler", IsBowler = true }
                }
            };

            Team pakistan = new Team
            {
                Name = "Pakistan",
                Players = new List<Player>
                {
                    new Player { Name = "Babar", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Rizwan", Role = "WK-Batsman", IsBowler = false },
                    new Player { Name = "Fakhar", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Imad", Role = "All-rounder", IsBowler = true },
                    new Player { Name = "Shaheen", Role = "Bowler", IsBowler = true },
                    new Player { Name = "Haris", Role = "Bowler", IsBowler = true },
                    new Player { Name = "Nawaz", Role = "All-rounder", IsBowler = false }
                }
            };

            Team australia = new Team
            {
                Name = "Australia",
                Players = new List<Player>
                {
                    new Player { Name = "Warner", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Smith", Role = "Batsman", IsBowler = false },
                    new Player { Name = "Maxwell", Role = "All-rounder", IsBowler = true },
                    new Player { Name = "Carey", Role = "WK-Batsman", IsBowler = false },
                    new Player { Name = "Starc", Role = "Bowler", IsBowler = true },
                    new Player { Name = "Cummins", Role = "Bowler", IsBowler = true },
                    new Player { Name = "Hazlewood", Role = "Bowler", IsBowler = false }
                }
            };

            List<Team> teams = new List<Team> { india, pakistan, australia };

            BallService ballService = new BallService();
            ScoreboardService scoreboard = new ScoreboardService();
            Random rnd = new Random();

            //FIRST MATCH 
            Team team1 = teams[rnd.Next(teams.Count)];
            teams.Remove(team1);
            Team team2 = teams[rnd.Next(teams.Count)];
            teams.Remove(team2);

            Console.WriteLine($"MATCH 1: {team1.Name} vs {team2.Name}\n");
            GetTeamInfo(team1,team2);
            int toss1 = tossService.DoToss(); // 0 or 1
            Team M1_battingFirst = toss1 == 0 ? team1 : team2;
            Team M1_bowlingFirst = toss1 == 0 ? team2 : team1;
            Console.WriteLine($"\n{M1_battingFirst.Name} won the toss and decided to BAT FIRST!!\n");

            Innings firstInnings = new Innings(M1_battingFirst, M1_bowlingFirst);
            PlayInnings(firstInnings, ballService, scoreboard);

            int target = firstInnings.TotalRuns + 1;
            Console.WriteLine($"\nTarget: {target}\n");

            Innings secondInnings = new Innings(M1_bowlingFirst, M1_battingFirst);
            secondInnings.Target = target;
            PlayInnings(secondInnings, ballService, scoreboard);

            Team winner1 = secondInnings.TotalRuns >= target ? M1_bowlingFirst : M1_battingFirst;
            Team loser1 = winner1 == M1_battingFirst ? M1_bowlingFirst : M1_battingFirst;

            Console.WriteLine($"\nWinner: {winner1.Name}");
            Console.WriteLine($"Loser: {loser1.Name}");

            ResetTeam(winner1);
            ResetTeam(loser1);
            // FINAL MATCH 
            Team team3 = teams[0];
            Console.WriteLine($"\nFINAL: {winner1.Name} vs {team3.Name}\n");
            int toss2 = tossService.DoToss(); // 0 or 1
            GetTeamInfo(winner1, team3);
            Team M2_battingFirst = toss2 == 0 ? winner1 : team3;
            Team M2_bowlingFirst = toss2 == 0 ? team3 : winner1;
            Console.WriteLine($"\n{M2_battingFirst.Name} won the toss and decided to BAT FIRST!!\n");

            Innings finalInnings1 = new Innings(M2_battingFirst, M2_bowlingFirst);
            PlayInnings(finalInnings1, ballService, scoreboard);

            int finalTarget = finalInnings1.TotalRuns + 1;
            Console.WriteLine($"\nTarget: {finalTarget}\n");

            Innings finalInnings2 = new Innings(M2_bowlingFirst, M2_battingFirst);
            finalInnings2.Target = finalTarget;
            PlayInnings(finalInnings2, ballService, scoreboard);

            Team champion =
                finalInnings2.TotalRuns >= finalTarget ? M2_bowlingFirst : M2_battingFirst;

            Console.WriteLine($"\n CHAMPION: {champion.Name}");

            Console.ReadLine();
        }
        static void ResetTeam(Team team)
        {
            foreach (var player in team.Players)
            {
                player.ResetForNewMatch();
            }
        }
        static void PlayInnings(Innings innings,BallService ballService,ScoreboardService scoreboard)
        {
            while (!innings.IsCompleted)
            {
                //int prevOver = innings.OversCompleted;

                // press Enter to bowl
                if (!innings.isOverInitiated)
                {
                    innings.SelectBowler();
                }
                Console.WriteLine("PRESS ENTER");
                Console.ReadLine();
                ballService.BowlBall(innings);

                //if (innings.OversCompleted > prevOver)
                //{
                //    innings.SelectBowler();
                //    scoreboard.ShowOverSummary(innings);
                //}
            }

            scoreboard.ShowInningsSummary(innings);
            // TARGET ACHIEVED (only for 2nd innings)
            if (innings.Target > 0 && innings.TotalRuns >= innings.Target)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"\nTarget achieved! {innings.BattingTeam.Name} wins!");
                Console.WriteLine($"{innings.BattingTeam.Name} won by {GameConfig.PlayersPerTeam - innings.Wickets}");
                Console.ResetColor();
                innings.IsCompleted = true;
                return;
            }
            if (innings.Target > 0 && innings.TotalRuns < innings.Target)
            {
                int margin = innings.Target - innings.TotalRuns - 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"{innings.BowlingTeam.Name} won by {margin} runs");
                Console.ResetColor();
            }
        }
    }
}