using src.Models;

namespace src.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll(int pageNumber, int pageSize);
        Task<Member> GetByID(int id);
        Task<Member> GetByEmail(string email);
        Task<Member> Create(Member member);
        Task<Member> Update(Member member);
        Task<bool> Delete(int id);
    }
}
