using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorageService.Storage.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string EMail { get; set; } = null!;

        [StringLength(100)]
        public string PasswordSalt { get; set; } = null!;

        [StringLength(100)]
        public string PasswordHash { get; set; } = null!;

        public bool Locked { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [StringLength(255)]
        public string Surname { get; set; } = null!;

        [StringLength(255)]
        public string? Patronymic { get; set; }

        [InverseProperty(nameof(AccountSession.Account))]
        public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();

    }
}
