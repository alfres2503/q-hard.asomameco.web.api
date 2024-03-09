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

        public async Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Role
                    .OrderBy(r => r.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => new Role 
                    { Id = r.Id,
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

        public async Task<Role> Update(Role role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return role;
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
