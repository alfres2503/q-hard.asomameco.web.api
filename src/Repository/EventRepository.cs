using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDBContext _context;

        public EventRepository(AppDBContext context)
        {
            _context = context;
        }

        // async method to get all events
        public async Task<Event> Create(Event _event)
        {
            try
            {
                await _context.Event.AddAsync(_event);
                await _context.SaveChangesAsync();

                if (this.GetByID(_event.Id) != null)
                    return _event;
                else
                    throw new Exception("Event not created");
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

        public async Task<bool> Delete(int id)
        {
            bool deletedStatus = false;
            try
            {
                Event _event = await _context.Event.FindAsync(id);

                if (_event == null)
                    return deletedStatus;

                _context.Event.Remove(_event);
                await _context.SaveChangesAsync();

                _event = await _context.Event.FindAsync(id);
                if (_event == null)
                    deletedStatus = true;

                return deletedStatus;
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

        public async Task<IEnumerable<Event>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Event
                    .OrderBy(e => e.Date)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(e => new Event
                    {
                        Id = e.Id,
                        IdMember = e.IdMember,
                        Name = e.Name,
                        Description = e.Description,
                        Date = e.Date,
                        Place = e.Place,
                        Time = e.Time,
                        IdCateringService = e.IdCateringService,
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

        public async Task<Event> GetByID(int id)
        {
            try
            {
                return await _context.Event
                    .Include(e => e.Member)
                    .Select(e => new Event
                    {
                        Id = e.Id,
                        IdMember = e.IdMember,
                        Name = e.Name,
                        Description = e.Description,
                        Date = e.Date,
                        Place = e.Place,
                        Time = e.Time,
                        IdCateringService = e.IdCateringService,
                    })
                    .FirstOrDefaultAsync(e => e.Id == id);
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

        public async Task<Event> Update(Event _event)
        {
            try
            {
                _context.Entry(_event).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _event;
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
                return await _context.Role.CountAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

    }
}
