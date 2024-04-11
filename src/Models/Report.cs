using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Models.Reports
{
    public class AttendancePercentageReport
    {
        public int TotalAssociates { get; set; }
        public double AverageAttendancePercentage { get; set; }
    }

}
