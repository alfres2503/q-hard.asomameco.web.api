using src.Models;

namespace src.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<int> GetCount(string searchTerm);
        Task<Member> GetByID(int id);
        Task<Member> GetByEmail(string email);
        Task<Member> GetByIdCard(string idCard);
        Task<Member> Create( Member member);
        Task<Member> Update(int id, Member member);
        Task<Member> ChangeState(int id);
        Task<bool> Delete(int id);
    }
}
