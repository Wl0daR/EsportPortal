using System;

namespace EsportPortal.Models
{
    public class TeamTournamentHistory
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public DateTime ParticipationDate { get; set; }
        public string LogoUrl { get; set; }
    }
}
