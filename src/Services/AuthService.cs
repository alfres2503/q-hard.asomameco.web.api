using src.Models;
using src.Utils;

namespace src.Services
{
    public class AuthService: IAuthService
    {
        public IMemberService _memberService;

        public AuthService(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<bool> Authenticate(Member pMember, string password)
        {
            try
            {
                return pMember.Password == Cryptography.EncryptAES(password);
            } catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
            
        }
    }
}
