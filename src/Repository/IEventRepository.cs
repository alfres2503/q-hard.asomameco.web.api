using src.Models;

namespace src.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll(int pageNumber, int pageSize);
        Task<Event> GetByID(int id);
        Task<Event> Create(Event _event);
        Task<Event> Update(Event _event);
        Task<bool> Delete(int id);
    }
}
