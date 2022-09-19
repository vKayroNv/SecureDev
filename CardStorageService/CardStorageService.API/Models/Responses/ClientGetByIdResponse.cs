using CardStorageService.API.Interfaces;
using CardStorageService.Core.Models;

namespace CardStorageService.API.Models.Responses
{
    public class ClientGetByIdResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public ClientDto? Client { get; set; }
    }
}
