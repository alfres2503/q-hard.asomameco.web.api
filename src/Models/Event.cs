using System.Text.Json.Serialization;

namespace src.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int IdMember { get; set; }
        public Member Member { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Place { get; set; }
        [JsonIgnore]
        public List<Attendance> Attendances { get; set; }
        [JsonIgnore]
        public List<Associate> Associates { get; set; }
    }
}
