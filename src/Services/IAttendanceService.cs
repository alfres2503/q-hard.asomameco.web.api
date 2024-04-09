using src.Models;

namespace src.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<int> GetCount(string searchTerm);
        Task<IEnumerable<Attendance>> GetByIdEvent(int id, int pageNumber, int pageSize);
        Task<IEnumerable<Attendance>> GetByIdAssociate(int id, int pageNumber, int pageSize);
        Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate);
        Task<Attendance> Create(Attendance attendance);
    }
}
