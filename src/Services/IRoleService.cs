using src.Models;

namespace src.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize);
        Task<int> GetCount();
        Task<Role> GetByID(int id);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task<bool> Delete(int id);
    }
}
