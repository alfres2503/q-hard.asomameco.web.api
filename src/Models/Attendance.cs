namespace src.Models
{
    public class Attendance
    {
        public int IdAssociate { get; set; }
        public int IdEvent { get; set; }
        public Associate Associate { get; set; }
        public Event Event { get; set; }
        public TimeOnly? ArrivalTime { get; set; }
        public bool isConfirmed { get; set; }
    }
}
