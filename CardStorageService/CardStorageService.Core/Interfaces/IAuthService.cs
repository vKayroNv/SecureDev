using CardStorageService.Core.Models;

namespace CardStorageService.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Register(AccountDto account, CancellationToken cts);

        public Task<string> Login(string email, string password, CancellationToken cts);
    }
}
