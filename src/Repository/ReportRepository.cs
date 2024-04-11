using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Models.Reports;
using src.Utils;

namespace src.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDBContext _context;

        public ReportRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<AttendancePercentageReport> GetAttendancePercentageData()
        {
            try
            {
                // Obtener la cantidad total de asociados
                var queryA = _context.Associate.AsQueryable();
                var totalAssociates =  await queryA.CountAsync(a => a.IsActive);

                // Obtener todos los eventos
                var queryE = _context.Event.AsQueryable();
                var allEvents = await queryE.ToListAsync();

                // Calcular la asistencia promedio para todos los eventos
                var totalAttendance = 0;
                foreach (var evt in allEvents)
                {
                    var attendanceCount = await _context.Attendance.CountAsync(a => a.IdEvent == evt.Id && a.isConfirmed);
                    totalAttendance += attendanceCount;
                }
                var averageAttendance = allEvents.Count > 0 ? (double)totalAttendance / allEvents.Count : 0;

                // Calcular el porcentaje de asistencia promedio
                var attendancePercentage = totalAssociates > 0 ? (averageAttendance / totalAssociates) * 100 : 0;

                // Crear el reporte de porcentaje de asistencia
                var report = new AttendancePercentageReport
                {
                    TotalAssociates = totalAssociates,
                    AverageAttendancePercentage = attendancePercentage
                };

                return report;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
