using src.Models;

namespace src.Services
{
    public interface IAuthService
    {
        Task<bool> Authenticate(Member pMember, string password);
    }
}
