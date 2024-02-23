using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class Attendance
    {
        [Required]
        public int IdAssociate { get; set; }
        [Required]
        public int IdEvent { get; set; }
        public Associate? Associate { get; set; }
        public Event? Event { get; set; }
        public TimeOnly? ArrivalTime { get; set; }
        [Required]
        public bool isConfirmed { get; set; }
    }
}
