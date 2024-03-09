using src.Models;

namespace src.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Task<Role> Create(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
