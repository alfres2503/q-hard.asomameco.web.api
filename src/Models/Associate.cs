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
        public List<Attendance> Attendances { get; set; }
        public List<Event> Events { get; set; }
    }
}
