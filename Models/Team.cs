using esport_portal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EsportPortal.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Player> Members { get; set; }
    }
}
