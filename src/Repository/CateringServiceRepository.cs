using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Repository
{
    public class CateringServiceRepository : ICateringServiceRepository
    {
        private readonly AppDBContext _context;

        public CateringServiceRepository(AppDBContext context)
        {
            _context = context;
        }

        // async method to get all Catering Services
        public async Task<IEnumerable<CateringService>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.CateringService
                    .OrderBy(cs => cs.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(cs => new CateringService
                    {
                        Id = cs.Id,
                        Email = cs.Email,
                        Phone = cs.Phone,
                        IsActive = cs.IsActive,
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
                return await _context.CateringService.CountAsync();
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

        public async Task<CateringService> GetByEmail(string email)
        {
            try
            {
                return await _context.CateringService
                    .FirstOrDefaultAsync(m => m.Email == email);
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

        public async Task<CateringService> GetByID(int id)
        {
            try
            {
                return await _context.CateringService
                    .Select(cs => new CateringService
                    {
                        Id = cs.Id,
                        Email = cs.Email,
                        Phone = cs.Phone,
                        IsActive = cs.IsActive,
                    })
                    .FirstOrDefaultAsync(m => m.Id == id);
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

        public async Task<CateringService> Create(CateringService catering_service)
        {
            try
            {
                await _context.CateringService.AddAsync(catering_service);
                await _context.SaveChangesAsync();

                if (this.GetByID(catering_service.Id) != null)
                    return catering_service;
                else
                    throw new Exception("Catering Service not created");
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
                CateringService catering_service = await _context.CateringService.FindAsync(id);

                if (catering_service == null)
                    return deletedStatus;

                _context.CateringService.Remove(catering_service);
                await _context.SaveChangesAsync();

                catering_service = await _context.CateringService.FindAsync(id);
                if (catering_service == null)
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

        public async Task<CateringService> ChangeState(int id)
        {
            try
            {
                _context.CateringService.Find(id).IsActive = !_context.CateringService.Find(id).IsActive;
                await _context.SaveChangesAsync();
                return await _context.CateringService.FindAsync(id);
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

        public async Task<CateringService> Update(CateringService catering_service)
        {
            try
            {
                _context.Entry(catering_service).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return catering_service;
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
