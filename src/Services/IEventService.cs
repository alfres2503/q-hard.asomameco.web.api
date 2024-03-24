using src.Models;

namespace src.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAll(int pageNumber, int pageSize);
        Task<Event> GetByID(int id);
        Task<int> GetCount();
        Task<Event> Create(Event _event);
        Task<Event> Update(Event _event);
        Task<bool> Delete(int id);
    }
}
