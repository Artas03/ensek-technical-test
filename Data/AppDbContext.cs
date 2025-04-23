using Ensek_Remote_Technical_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek_Remote_Technical_Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .Property(a => a.AccountId)
                .ValueGeneratedNever();
        }
    }
}
