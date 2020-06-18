using Microsoft.EntityFrameworkCore;
using Somi92.SkydivingLogbook.Domain.Data.Configuration;
using Somi92.SkydivingLogbook.Domain.Model;

namespace Somi92.SkydivingLogbook.Domain.Data
{
    public class SkydivingLogbookContext : DbContext
    {
        public SkydivingLogbookContext(DbContextOptions<SkydivingLogbookContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User> users { get; set; }
    }
}
