using src.Models;
using src.Models.Reports;
using src.Repository;

namespace src.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _ReportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _ReportRepository = reportRepository;
        }

        public async Task<AttendancePercentageReport> GetAttendancePercentageData()
        {
            try { return await _ReportRepository.GetAttendancePercentageData().ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
