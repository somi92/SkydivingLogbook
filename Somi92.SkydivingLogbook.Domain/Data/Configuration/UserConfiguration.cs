using System;
using Microsoft.EntityFrameworkCore;
using Somi92.SkydivingLogbook.Domain.Model;

namespace Somi92.SkydivingLogbook.Domain.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable(nameof(User))
                .HasKey(e => e.Id);
        }
    }
}
