namespace CardStorageService.API.Models.Requests
{
    public class AuthLoginRequest
    {
        public string EMail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}