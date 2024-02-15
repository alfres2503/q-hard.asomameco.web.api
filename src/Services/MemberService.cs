using src.Models;
using src.Repository;

namespace src.Services
{
    public class MemberService : IMemberService
    {
        private readonly MemberRepository _memberRepository;

        public MemberService(MemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public MemberService(AppDBContext context)
        {
            _memberRepository = new MemberRepository(context);
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            return await _memberRepository.GetAll();
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _memberRepository.GetByEmail(email);
        }

        public async Task<Member> GetByID(int id)
        {
            return await _memberRepository.GetByID(id);
        }
        public Member Add(Member member)
        {
            return _memberRepository.Add(member);
        }

        public Member Update(Member member)
        {
            return _memberRepository.Update(member);
        }

        public bool Delete(int id)
        {
            return _memberRepository.Delete(id);
        }

    }
}
