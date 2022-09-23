using CardStorageService.API.Interfaces;

namespace CardStorageService.API.Models.Responses
{
    public class AuthLoginResponse : IOperationResult
    {
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }
    }
}