using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
