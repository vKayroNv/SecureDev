namespace CardStorageService.API.Models.Requests
{
    public class AuthRegisterRequest
    {
        public string EMail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
    }
}