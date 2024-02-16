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
        Task<Member> Add(Member member);
        Member Update(Member member);
        bool Delete(int id);
    }
}
