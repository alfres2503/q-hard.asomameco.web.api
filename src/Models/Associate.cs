using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models
{
    public class Associate
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IdCard { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

    }
}
