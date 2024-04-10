using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Migrations;
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

        // async method to get all Attendance

        public async Task<IEnumerable<Attendance>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy)
        {

            try
            {
                var query = _context.Attendance.AsQueryable();

                // if there is a search term, filter the query
                if (!string.IsNullOrEmpty(searchTerm))
                    query = query.Where(cs => cs.Event.Name.Contains(searchTerm) || cs.Associate.Name.Contains(searchTerm));

                // if there is an orderBy parameter, order the query
                switch (orderBy)
                {
                    case "id":
                        query = query.OrderBy(a => a.IdEvent);
                        break;
                    case "id_desc":
                        query = query.OrderByDescending(a => a.IdEvent);
                        break;
                    case "name":
                        query = query.OrderBy(a => a.Event.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(a => a.Event.Name);
                        break;
                    case "role":
                        query = query.OrderBy(a => a.IdAssociate);
                        break;
                    case "role_desc":
                        query = query.OrderByDescending(cs => cs.IdAssociate);
                        break;
                    case "active":
                        query = query.OrderBy(a => a.isConfirmed);
                        break;
                    case "active_desc":
                        query = query.OrderByDescending(a => a.isConfirmed);
                        break;
                    default:
                        query = query.OrderBy(a => a.IdEvent);
                        break;
                }

                return await query
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

        public async Task<int> GetCount(string searchTerm)
        {
            try
            {
                // if there is a search term, return the count of the filtered query
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var query = _context.Attendance.AsQueryable();
                    return await query.Where(a => a.Event.Name.Contains(searchTerm) || a.Associate.Name.Contains(searchTerm)).CountAsync();

                }

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

        public async Task<Attendance> ChangeState(int idEvent, int idAssociate)
        {
            try
            {
                _context.Attendance.Find(idEvent, idAssociate).isConfirmed = !_context.Attendance.Find(idEvent, idAssociate).isConfirmed;
                await _context.SaveChangesAsync();
                return await _context.Attendance.FindAsync(idEvent, idAssociate);
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
