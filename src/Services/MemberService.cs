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

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public IEnumerable<Member> GetAll()
        {
 
            return _memberRepository.GetAll();
        }

        public Member GetByEmail(string email)
        {
            return _memberRepository.GetByEmail(email);
        }

        public Member GetByID(int id)
        {
            return _memberRepository.GetByID(id);
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
