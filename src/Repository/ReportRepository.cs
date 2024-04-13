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

        public async Task<IEnumerable<EventAttendanceReport>> GetEventAttendanceTrend()
        {
            try
            {
                // Obtener todos los eventos ordenados por fecha
                var events = await _context.Event.ToListAsync();

                // Generar el reporte de asistencia para cada evento
                var attendanceTrend = new List<EventAttendanceReport>();
                foreach (var evt in events)
                {
                    var attendanceCount = await _context.Attendance.CountAsync(a => a.IdEvent == evt.Id && a.isConfirmed);
                    attendanceTrend.Add(new EventAttendanceReport
                    {
                        Name = evt.Name,
                        AttendanceCount = attendanceCount
                    });
                }

                return attendanceTrend;
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

        public async Task<IEnumerable<MonthlyEventsReport>> GetMonthlyEventsReport()
        {
            try
            {
                // Obtener la cantidad de eventos por mes
                var monthlyEvents = await _context.Event
                    .Where(e => e.Date.Year == DateTime.Now.Year)
                    .GroupBy(e => e.Date.Month)
                    .Select(group => new MonthlyEventsReport
                    {
                        Month = group.Key,
                        EventsCount = group.Count()
                    })
                    .OrderBy(report => report.Month)
                    .ToListAsync();

                return monthlyEvents;
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

        public async Task<MemberEventsCoveredReport> GetMemberWithMostEventsCovered()
        {
            // Agrupar eventos por miembro y contarlos
            var memberEventCounts = await _context.Event
                .GroupBy(e => e.IdMember)
                .Select(group => new
                {
                    MemberId = group.Key,
                    EventsCovered = group.Count()
                })
                .ToListAsync();

            // Encontrar el miembro con la mayor cantidad de eventos cubiertos
            var topMemberEventCount = memberEventCounts.OrderByDescending(m => m.EventsCovered).FirstOrDefault();

            if (topMemberEventCount != null)
            {
                // Obtener la información del miembro
                var topMember = await _context.Member
                    .Where(m => m.Id == topMemberEventCount.MemberId)
                    .Select(m => new MemberEventsCoveredReport
                    {
                        MemberName = $"{m.FirstName} {m.LastName}",
                        EventsCovered = topMemberEventCount.EventsCovered
                    })
                    .FirstOrDefaultAsync();

                return topMember;
            }

            return null;
        }

    }
}
