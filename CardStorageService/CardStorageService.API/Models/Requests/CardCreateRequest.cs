﻿namespace CardStorageService.API.Models.Requests
{
    public class CardCreateRequest
    {
        public string CardNo { get; set; } = null!;
        public string? Name { get; set; }
        public string? CVV2 { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
