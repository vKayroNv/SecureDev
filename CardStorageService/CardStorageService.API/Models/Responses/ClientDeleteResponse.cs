using CardStorageService.API.Interfaces;

namespace CardStorageService.API.Models.Responses
{
    public class ClientDeleteResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public int? Count { get; set; }
    }
}
