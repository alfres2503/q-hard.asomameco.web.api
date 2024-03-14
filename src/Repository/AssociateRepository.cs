using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;
using System.ComponentModel.DataAnnotations;

namespace src.Repository
{
    public class AssociateRepository : IAssociateRepository
    {
        private readonly AppDBContext _context;

        public AssociateRepository(AppDBContext context)
        {
            _context = context;
        }

        // async method to get all Catering Services
        public async Task<IEnumerable<Associate>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Associate
                    .OrderBy(a => a.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new Associate
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Email = a.Email,
                        Phone = a.Phone,
                        IsActive = a.IsActive,
                        IdCard = a.IdCard,
                    })
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<int> GetCount()
        {
            try
            {
                return await _context.Associate.CountAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Associate> GetByEmail(string email)
        {
            try
            {
                return await _context.Associate
                    .FirstOrDefaultAsync(a => a.Email == email);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Associate> GetByID(int id)
        {
            try
            {
                return await _context.Associate
                    .Select(a => new Associate
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Email = a.Email,
                        Phone = a.Phone,
                        IsActive = a.IsActive,
                        IdCard = a.IdCard,
                    })
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Associate> Create(Associate associate)
        {
            try
            {
                await _context.Associate.AddAsync(associate);
                await _context.SaveChangesAsync();

                if (this.GetByID(associate.Id) != null)
                    return associate;
                else
                    throw new Exception("Associate not created");
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            bool deletedStatus = false;
            try
            {
                Associate associate = await _context.Associate.FindAsync(id);

                if (associate == null)
                    return deletedStatus;

                _context.Associate.Remove(associate);
                await _context.SaveChangesAsync();

                associate = await _context.Associate.FindAsync(id);
                if (associate == null)
                    deletedStatus = true;

                return deletedStatus;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Associate> ChangeState(int id)
        {
            try
            {
                _context.Associate.Find(id).IsActive = !_context.Associate.Find(id).IsActive;
                await _context.SaveChangesAsync();
                return await _context.Associate.FindAsync(id);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }

        }

        public async Task<Associate> Update(Associate associate)
        {
            try
            {
                _context.Entry(associate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return associate;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
