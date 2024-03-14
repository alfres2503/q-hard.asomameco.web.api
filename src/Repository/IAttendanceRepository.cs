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
        Task<IEnumerable<Attendance>> GetAll(int pageNumber, int pageSize);
        Task<int> GetCount();
        Task<IEnumerable<Attendance>> GetByIdEvent(int id,int pageNumber, int pageSize);
        Task<IEnumerable<Attendance>> GetByIdAssociate(int id, int pageNumber, int pageSize);
        Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate);
        Task<Attendance> Create(Attendance attendance);
    }
}
