using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repository
{
    public interface IMemberRepository 
    {
        Task<IEnumerable<Member>> GetAll(int pageNumber, int pageSize, string searchTerm, string orderBy);
        Task<int> GetCount(string searchTerm);
        Task<Member> GetByID(int id);
        Task<Member> GetByEmail(string email);
        Task<Member> GetByIdCard(string idCard);
        Task<Member> Create(Member member);
        Task<Member> Update(int id, Member member);
        Task<Member> ChangeState(int id);
        Task<bool> Delete(int id);
    }
}
