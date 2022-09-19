using CardStorageService.Core.Models;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Interfaces
{
    public interface ICardService : IStorageService<Card, CardDto, string>
    {
        Task<IReadOnlyList<CardDto>> GetByClientId(int id, CancellationToken cts);
    }
}
