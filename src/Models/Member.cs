using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public int IdRole { get; set; }
        [Required]
        public string IdCard { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Role? Role { get; set; }
    }
}
