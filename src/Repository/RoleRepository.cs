using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDBContext _context;

        public RoleRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Role> Create(Role role)
        {
            try
            {
                await _context.Role.AddAsync(role);
                await _context.SaveChangesAsync();

                if (this.GetByID(role.Id) != null)
                    return role;
                else
                    throw new Exception("Role not created");

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception ($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            bool deletedStatus = false;
            try
            {
                Role role = await _context.Role.FindAsync(id);

                if (role == null)
                    return deletedStatus;

                _context.Role.Remove(role);
                await _context.SaveChangesAsync();

                role = await _context.Role.FindAsync(id);

                if (role == null)
                    deletedStatus = true;

                return deletedStatus;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }

        }

        public async Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy)
        {
            try
            {
                var query = _context.Role.AsQueryable();

                // if there is a search term, filter the query
                if (!string.IsNullOrEmpty(searchTerm))
                    query = query.Where(r => r.Description.Contains(searchTerm));

                // if there is an orderBy parameter, order the query
                switch (orderBy)
                {
                    case "id":
                        query = query.OrderBy(r => r.Id);
                        break;
                    case "id_desc":
                        query = query.OrderByDescending(r => r.Id);
                        break;
                    case "name":
                        query = query.OrderBy(r => r.Description);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(r => r.Description);
                        break;
                    default:
                        query = query.OrderBy(m => m.Id);
                        break;
                }

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => new Role
                    {
                        Id = r.Id,
                        Description = r.Description
                    })
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<Role> GetByID(int id)
        {
            try
            {
                return await _context.Role
                    .Select(r => new Role
                    {
                        Id = r.Id,
                        Description = r.Description
                    })
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<int> GetCount(string searchTerm)
        {
            try
            {
                // if there is a search term, return the count of the filtered query
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var query = _context.Role.AsQueryable();
                    return await query.Where(r => r.Description.Contains(searchTerm)).CountAsync();
                }

                return await _context.Role.CountAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<Role> Update(int id, Role role)
        {
            try
            {
                _context.Role.Find(id).Id = role.Id;
                _context.Role.Find(id).Description = role.Description;

                await _context.SaveChangesAsync();
                return await _context.Role
                    .Select(r => new Role
                    {
                        Id = r.Id,
                        Description = r.Description
                    })
                    .FirstOrDefaultAsync(r => r.Id == id);
                    ;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }
    }
}
