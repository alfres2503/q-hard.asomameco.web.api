using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repository
{
    public interface IAssociateRepository
    {
        Task<IEnumerable<Associate>> GetAll(int pageNumber, int pageSize);
        Task<int> GetCount();
        Task<Associate> GetByID(int id);
        Task<Associate> GetByEmail(string email);
        Task<Associate> Create(Associate associate);
        Task<Associate> Update(Associate associate);
        Task<Associate> ChangeState(int id);
        Task<bool> Delete(int id);
    }
}
