using src.Models;

namespace src.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll();
        Task<Member> GetByID(int id);
        Task<Member> GetByEmail(string email);
        Task<Member> Add(Member member);
        Member Update(Member member);
        bool Delete(int id);
    }
}
