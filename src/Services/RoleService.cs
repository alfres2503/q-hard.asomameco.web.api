using src.Models;
using src.Repository;
using System.Data;

namespace src.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> Create(Role role)
        {
            try
            {
                return await _roleRepository.Create(role).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _roleRepository.Delete(id);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _roleRepository.GetAll(pageNumber, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Role> GetByID(int id)
        {
            try
            {
                return await _roleRepository.GetByID(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Role> Update(Role role)
        {
            try
            {
                return await _roleRepository.Update(role);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }
    }
}
