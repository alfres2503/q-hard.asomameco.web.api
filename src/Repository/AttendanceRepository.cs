using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Models;
using System.ComponentModel.DataAnnotations;

namespace src.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDBContext _context;

        public AttendanceRepository(AppDBContext context)
        {
            _context = context;
        }

        // async method to get all Catering Services
        public async Task<IEnumerable<Attendance>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Attendance
                    .OrderBy(a => a.IdEvent)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new Attendance
                    {
                        IdAssociate = a.IdAssociate,
                        IdEvent = a.IdEvent,
                        ArrivalTime = a.ArrivalTime,
                        isConfirmed = a.isConfirmed,
                        Associate = a.Associate,
                        Event = a.Event,
                    })
                    .ToListAsync();
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

        public async Task<int> GetCount()
        {
            try
            {
                return await _context.Attendance.CountAsync();
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

        public async Task<IEnumerable<Attendance>> GetByIdEvent(int id, int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Attendance
                    .Where(a => a.IdEvent == id)
                    .OrderBy(a => a.IdEvent)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new Attendance
                    {
                        IdAssociate = a.IdAssociate,
                        IdEvent = a.IdEvent,
                        ArrivalTime = a.ArrivalTime,
                        isConfirmed = a.isConfirmed,
                        Associate = a.Associate,
                        Event = a.Event,
                    })
                    .ToListAsync();
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

        public async Task<IEnumerable<Attendance>> GetByIdAssociate(int id, int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Attendance
                    .Where(a => a.IdAssociate == id)
                    .OrderBy(a => a.IdAssociate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new Attendance
                    {
                        IdAssociate = a.IdAssociate,
                        IdEvent = a.IdEvent,
                        ArrivalTime = a.ArrivalTime,
                        isConfirmed = a.isConfirmed,
                        Associate = a.Associate,
                        Event = a.Event,
                    })
                    .ToListAsync();
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

        public async Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate)
        {
            try
            {
                return await _context.Attendance
                    .Select(a => new Attendance
                    {
                        IdAssociate = a.IdAssociate,
                        IdEvent = a.IdEvent,
                        ArrivalTime = a.ArrivalTime,
                        isConfirmed = a.isConfirmed,
                        Associate = a.Associate,
                        Event = a.Event,
                    })
                    .FirstOrDefaultAsync(a => a.IdEvent == idEvent && a.IdAssociate == idAssociate);
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

        public async Task<Attendance> Create(Attendance attendance)
        {
            try
            {
                await _context.Attendance.AddAsync(attendance);
                await _context.SaveChangesAsync();

                if (this.GetByIdEventIdAssociate(attendance.IdEvent, attendance.IdAssociate) != null)
                    return attendance;
                else
                    throw new Exception("Attendance not created");
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
