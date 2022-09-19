using CardStorageService.Core.Models;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Interfaces
{
    public interface IClientService : IStorageService<Client, ClientDto, int>
    {
    }
}
