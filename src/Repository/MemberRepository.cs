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
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Member.ToListAsync();
        }

        public IEnumerable<Member> GetAll()
        {
            IEnumerable<Member> list = null;
            try
            {
                list = _context.Member
                    .Include(m => m.Role)
                    .ToList<Member>();

                return list;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
            }
        }

        public Member GetByEmail(string email)
        {
            Member member = null;
            try
            {
                member = _context.Member
                    .Include(m => m.Role)
                    .Where(m => m.Email == email)
                    .FirstOrDefault();

                return member;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
            }
        }

        public Member GetByID(int id)
        {
            Member member = null;
            try
            {
                member = _context.Member
                    .Include(m => m.Role)
                    .Where(m => m.Id == id)
                    .FirstOrDefault();

                return member;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
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
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
            }
        }

        public bool Delete(int id)
        {
            bool deletedStatus = false;
            try
            {
                Member member = _context.Member.Find(id);

                if(member == null)
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
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
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
                throw new Exception("Database error: ", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: ", ex);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
