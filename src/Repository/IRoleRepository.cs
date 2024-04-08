﻿using src.Models;

namespace src.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<int> GetCount(string searchTerm);
        Task<Role> GetByID(int id);
        Task<Role> Create(Role role);
        Task<Role> Update(int id, Role role);
        Task<bool> Delete(int id);
    }
}
