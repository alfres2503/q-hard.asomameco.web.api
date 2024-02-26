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

        public async Task<IEnumerable<Member>> GetAll(int pageNumber, int pageSize)
        {
            try { return await _memberRepository.GetAll(pageNumber, pageSize).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
            
        }

        public async Task<Member> GetByEmail(string email)
        {
            try { return await _memberRepository.GetByEmail(email).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Member> GetByID(int id)
        {
            try { return await _memberRepository.GetByID(id).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }
        public async Task<Member> Create(Member member)
        {
            try { return await _memberRepository.Create(member).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Member> Update(Member member)
        {
            try { return await _memberRepository.Update(member); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try { return await _memberRepository.Delete(id); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
