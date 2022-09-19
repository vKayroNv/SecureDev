using CardStorageService.API.Interfaces;
using CardStorageService.Core.Models;

namespace CardStorageService.API.Models.Responses
{
    public class CardGetAllResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public IReadOnlyCollection<CardDto>? Cards { get; set; }
    }
}
