namespace CardStorageService.API.Models.Requests
{
    public class ClientUpdateRequest
    {
        public int ClientId { get; set; }
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Patronymic { get; set; }
    }
}
