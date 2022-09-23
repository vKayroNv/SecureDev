using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorageService.Storage.Models
{
    [Table("AccountSessions")]
    public class AccountSession
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [StringLength(384)]
        public string SessionToken { get; set; } = null!;

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeLastRequest { get; set; }

        public bool IsClosed => TimeClosed < DateTime.Now;

        [Column(TypeName = "datetime2")]
        public DateTime? TimeClosed { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
