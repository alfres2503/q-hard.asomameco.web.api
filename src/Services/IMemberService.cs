using src.Models;

namespace src.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        IEnumerable<Member> GetAll();
        Member GetByID(int id);
        Member GetByEmail(string email);
        Member Add(Member member);
        Member Update(Member member);
        bool Delete(int id);
    }
}
