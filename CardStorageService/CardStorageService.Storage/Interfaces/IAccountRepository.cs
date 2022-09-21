using CardStorageService.Storage.Models;

namespace CardStorageService.Storage.Interfaces
{
    public interface IAccountRepository : IRepository<Account, int>
    {
        Task<Account> GetByEmail(string email, CancellationToken cts);
    }
}
