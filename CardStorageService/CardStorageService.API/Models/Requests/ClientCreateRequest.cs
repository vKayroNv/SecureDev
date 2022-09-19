namespace CardStorageService.API.Models.Requests
{
    public class ClientCreateRequest
    {
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
    }
}
