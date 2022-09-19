using CardStorageService.API.Interfaces;
using CardStorageService.Core.Models;

namespace CardStorageService.API.Models.Responses
{
    public class ClientGetAllResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public IReadOnlyCollection<ClientDto>? Clients { get; set; }
    }
}
