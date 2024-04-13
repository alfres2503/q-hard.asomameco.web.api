using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models.Reports
{
    public class AttendancePercentageReport
    {
        public int TotalAssociates { get; set; }
        public double AverageAttendancePercentage { get; set; }
    }

    public class EventAttendanceReport
    {
        public string Name { get; set; }
        public int AttendanceCount { get; set; }
    }

    public class MonthlyEventsReport
    {
        public int Month { get; set; }
        public int EventsCount { get; set; }
    }

    public class MemberEventsCoveredReport
    {
        public string MemberName { get; set; }
        public int EventsCovered { get; set; }
    }

}
