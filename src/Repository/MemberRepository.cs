using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDBContext _context;

        public MemberRepository(AppDBContext context)
        {
            _context = context;
        }

        // async method to get all members
        public async Task<IEnumerable<Member>> GetAll()
        {
            try
            {
                return await _context.Member.ToListAsync();
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

        public async Task<Member> GetByEmail(string email)
        {
            try
            {
                return await _context.Member
                    .Include(m => m.Role)
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

        public async Task<Member> GetByID(int id)
        {
            try
            {
                return await _context.Member
                    .Include(m => m.Role)
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

        public Member Add(Member member)
        {
            try
            {
                _context.Member.Add(member);
                _context.SaveChanges();
                return member;
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

        public bool Delete(int id)
        {
            bool deletedStatus = false;
            try
            {
                Member member = _context.Member.Find(id);

                if (member == null)
                    return deletedStatus;

                _context.Member.Remove(member);
                _context.SaveChanges();

                member = _context.Member.Find(id);
                if (member == null)
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

        public Member Update(Member member)
        {
            try
            {
                _context.Entry(member).State = EntityState.Modified;
                _context.SaveChanges();
                return member;
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
