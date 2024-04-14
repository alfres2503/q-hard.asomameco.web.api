using src.Models;

namespace src.Services
{
    public interface IAttendanceService
    {
        Task<int> GetCount(string searchTerm);
        Task<IEnumerable<Attendance>> GetByIdEvent(int id, int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate);
        Task<Attendance> Create(Attendance attendance);
    }
}
