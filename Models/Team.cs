using System.Collections.Generic;

namespace EsportPortal.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<PlayerHistory> PlayerHistories { get; set; }
        public ICollection<TeamTournamentHistory> TeamTournamentHistories { get; set; }
    }
}
