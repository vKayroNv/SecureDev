namespace CardStorageService.API.Models.Requests
{
    public class CardUpdateRequest
    {
        public string CardId { get; set; } = null!;
        public int ClientId { get; set; }
        public string CardNo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CVV2 { get; set; } = null!;
        public DateTime ExpDate { get; set; }
    }
}
