using src.Models;

namespace src.Services
{
    public class EventService : IEventService
    {
        public Task<Event> Create(Event _event)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Update(Event _event)
        {
            throw new NotImplementedException();
        }
    }
}
