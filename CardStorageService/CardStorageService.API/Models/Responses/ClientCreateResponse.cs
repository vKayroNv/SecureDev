using CardStorageService.API.Interfaces;

namespace CardStorageService.API.Models.Responses
{
    public class ClientCreateResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public int? ClientId { get; set; }
    }
}
