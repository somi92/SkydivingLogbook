using Microsoft.EntityFrameworkCore;
using Somi92.SkydivingLogbook.Core.Data.Configuration;
using Somi92.SkydivingLogbook.Core.Domain;

namespace Somi92.SkydivingLogbook.Core.Data
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
