using src.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repository
{
    public interface IReportRepository
    {
        Task<AttendancePercentageReport> GetAttendancePercentageData();
        Task<IEnumerable<EventAttendanceReport>> GetEventAttendanceTrend();
        Task<IEnumerable<MonthlyEventsReport>> GetMonthlyEventsReport();
        Task<MemberEventsCoveredReport> GetMemberWithMostEventsCovered();

    }
}
