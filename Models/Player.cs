using System.ComponentModel.DataAnnotations;

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
    }
}
