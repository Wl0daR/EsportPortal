using System;
using System.Linq;

namespace EsportPortal.Models
{
    public static class DbInitializer
    {
        public static void Initialize(EsportContext context)
        {
            context.Database.EnsureCreated();

            // Sprawdź, czy już mamy jakieś dane
            if (context.Tournaments.Any())
            {
                return;   // Baza została już zainicjalizowana
            }

            var tournaments = new Tournament[]
            {
                new Tournament { Name = "Intel Extreme Masters", Date = DateTime.Parse("2024-03-05"), Location = "Katowice" },
                new Tournament { Name = "ESL One", Date = DateTime.Parse("2024-06-15"), Location = "Kolonia" }
            };
            foreach (Tournament t in tournaments)
            {
                context.Tournaments.Add(t);
            }
            context.SaveChanges();

            var teams = new Team[]
            {
                new Team { Name = "Team Liquid" },
                new Team { Name = "Fnatic" }
            };
            foreach (Team te in teams)
            {
                context.Teams.Add(te);
            }
            context.SaveChanges();

            var players = new Player[]
            {
                new Player { Name = "Player1", TeamId = teams.Single(s => s.Name == "Team Liquid").Id, Role = "Entry Fragger" },
                new Player { Name = "Player2", TeamId = teams.Single(s => s.Name == "Team Liquid").Id, Role = "AWPer" },
                new Player { Name = "Player3", TeamId = teams.Single(s => s.Name == "Fnatic").Id, Role = "Lurker" },
                new Player { Name = "Player4", TeamId = teams.Single(s => s.Name == "Fnatic").Id, Role = "Support" }
            };
            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();
        }
    }
}
