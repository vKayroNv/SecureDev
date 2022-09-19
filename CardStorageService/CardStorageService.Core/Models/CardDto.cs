namespace CardStorageService.Core.Models
{
    public class CardDto
    {
        public Guid CardId { get; set; }
        public string CardNo { get; set; } = null!;
        public string? Name { get; set; }
        public string? CVV2 { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
