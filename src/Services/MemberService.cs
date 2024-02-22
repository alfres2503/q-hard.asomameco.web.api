using src.Models;
using src.Repository;

namespace src.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            return await _memberRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _memberRepository.GetByEmail(email).ConfigureAwait(false);
        }

        public async Task<Member> GetByID(int id)
        {
            return await _memberRepository.GetByID(id).ConfigureAwait(false);
        }
        public async Task<Member> Create(Member member)
        {
            return await _memberRepository.Create(member).ConfigureAwait(false);
        }

        public async Task<Member> Update(Member member)
        {
            return await _memberRepository.Update(member);
        }

        public async Task<bool> Delete(int id)
        {
            return await _memberRepository.Delete(id);
        }

    }
}
