using System;
using System.ComponentModel.DataAnnotations;

namespace EsportPortal.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
