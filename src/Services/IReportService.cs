using src.Models;
using src.Models.Reports;

namespace src.Services
{
    public interface IReportService
    {
        Task<AttendancePercentageReport> GetAttendancePercentageData();
        Task<IEnumerable<EventAttendanceReport>> GetEventAttendanceTrend();
        Task<IEnumerable<MonthlyEventsReport>> GetMonthlyEventsReport();
        Task<MemberEventsCoveredReport> GetMemberWithMostEventsCovered();
    }
}
