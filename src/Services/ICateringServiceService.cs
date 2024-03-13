using src.Models;

namespace src.Services
{
    public interface ICateringServiceService
    {
        Task<IEnumerable<CateringService>> GetAll(int pageNumber, int pageSize);
        Task<int> GetCount();
        Task<CateringService> GetByID(int id);
        Task<CateringService> GetByEmail(string email);
        Task<CateringService> Create(CateringService catering_service);
        Task<CateringService> Update(CateringService catering_service);
        Task<CateringService> ChangeState(int id);
        Task<bool> Delete(int id);
    }
}
