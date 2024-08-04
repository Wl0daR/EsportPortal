using System.Collections.Generic;
namespace EsportPortal.Models

{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Surname { get; set; }
        public int? TeamId { get; set; }
        public TeamDto Team { get; set; }
        public string Role { get; set; }
        public string BirthDate { get; set; }
        public string Nationality { get; set; }
        public string FavouriteMap { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
