using src.Models;
using src.Models.Reports;

namespace src.Services
{
    public interface IReportService
    {
        Task<AttendancePercentageReport> GetAttendancePercentageData();
    }
}
