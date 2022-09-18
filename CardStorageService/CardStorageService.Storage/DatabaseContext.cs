using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;

        public DatabaseContext(DbContextOptions options) : base(options) { }
    }
}
