using CardStorageService.API.Interfaces;

namespace CardStorageService.API.Models.Responses
{
    public class CardCreateResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public string? CardId { get; set; }
    }
}
