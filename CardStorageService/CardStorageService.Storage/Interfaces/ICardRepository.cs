using CardStorageService.Storage.Models;

namespace CardStorageService.Storage.Interfaces
{
    public interface ICardRepository : IRepository<Card, string>
    {
        Task<IReadOnlyList<Card>> GetByClientId(int id, CancellationToken cts);
    }
}
