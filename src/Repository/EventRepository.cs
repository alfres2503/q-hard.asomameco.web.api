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

        public async Task<IEnumerable<Event>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy)
        {
            try
            {
                var query = _context.Event.AsQueryable();

                // if there is a search term, filter the query
                if (!string.IsNullOrEmpty(searchTerm))
                    query = query.Where(m => m.Name.Contains(searchTerm) || m.Place.Contains(searchTerm) || m.Date.ToString().Contains(searchTerm) || m.Description.Contains(searchTerm));

                // if there is an orderBy parameter, order the query
                switch (orderBy)
                {
                    case "id":
                        query = query.OrderBy(m => m.Id);
                        break;
                    case "member":
                        query = query.OrderByDescending(m => m.IdMember);
                        break;
                    case "name":
                        query = query.OrderBy(m => m.Name);
                        break;
                    case "date_asc":
                        query = query.OrderBy(m => m.Date);
                        break;
                    case "date_desc":
                        query = query.OrderByDescending(m => m.Date);
                        break;
                    case "place":
                        query = query.OrderBy(m => m.Place);
                        break;
                    default:
                        query = query.OrderBy(m => m.Id);
                        break;
                }

                return await query
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

        public async Task<Event> Update(int id, Event _event)
        {
            try
            {
                _context.Event.Find(id).IdMember = _event.IdMember;
                _context.Event.Find(id).Name = _event.Name;
                _context.Event.Find(id).Description = _event.Description;
                _context.Event.Find(id).Date = _event.Date;
                _context.Event.Find(id).Time = _event.Time;
                _context.Event.Find(id).Place = _event.Place;
                _context.Event.Find(id).IdCateringService = _event.IdCateringService;

                await _context.SaveChangesAsync();
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
                   .FirstOrDefaultAsync(m => m.Id == id);
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
