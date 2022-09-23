using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage
{
    public class DatabaseContext : DbContext
    {
        // dotnet user-secrets init
        // dotnet user-secrets set "ConnectionString" "data source=<host>;initial catalog=<dbname>;User Id=<username>;Password=<pass>;MultipleActiveResultSets=True;App=EntityFramework"

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountSession> AccountSessions { get; set; } = null!;

        public DatabaseContext(DbContextOptions options) : base(options) { }
    }
}
