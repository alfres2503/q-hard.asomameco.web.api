using System.Text.Json.Serialization;

namespace src.Models
{
    public class Associate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdCard { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public List<Attendance> Attendances { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
