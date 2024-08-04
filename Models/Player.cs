using System.Collections.Generic;

namespace EsportPortal.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Surname { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string FavouriteMap { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<PlayerHistory> PlayerHistories { get; set; }
        public ICollection<TeamTournamentHistory> TeamTournamentHistories { get; set; }
    }
}
