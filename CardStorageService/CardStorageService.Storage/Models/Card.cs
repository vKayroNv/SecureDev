﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorageService.Storage.Models
{
    [Table("Cards")]
    public class Card
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Column]
        [StringLength(20)]
        public string CardNo { get; set; } = null!;

        [Column]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column]
        [StringLength(50)]
        public string? CVV2 { get; set; }

        [Column]
        public DateTime ExpDate { get; set; }

        public virtual Client Client { get; set; } = null!;
    }
}
