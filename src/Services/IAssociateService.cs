using src.Models;

namespace src.Services
{
    public interface IAssociateService
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
