using CardStorageService.API.Interfaces;
using CardStorageService.Core.Models;

namespace CardStorageService.API.Models.Responses
{
    public class CardGetByIdResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public CardDto? Card { get; set; }
    }
}
