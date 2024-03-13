using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models
{
    public class Event
    {

        public int Id { get; set; }
        [Required]
        public int IdMember { get; set; }
        public Member? Member { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly Time { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public int IdCateringService { get; set; }
        public CateringService? CateringService { get; set; }
    }
}
