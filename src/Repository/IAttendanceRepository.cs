using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repository
{
    public interface IAttendanceRepository
    {
        Task<int> GetCount(string searchTerm);
        Task<IEnumerable<Attendance>> GetByIdEvent(int id, int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate);
        Task<Attendance> Create(Attendance attendance);
    }
}
