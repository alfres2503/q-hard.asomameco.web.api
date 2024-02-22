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
        Task<IEnumerable<Member>> GetAll();
        Task<Member> GetByID(int id);
        Task<Member> GetByEmail(string email);
        Task<Member> Create(Member member);
        Task<Member> Update(Member member);
        Task<bool> Delete(int id);
    }
}
