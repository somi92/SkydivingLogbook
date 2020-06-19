using System;
using Microsoft.EntityFrameworkCore;
using Somi92.SkydivingLogbook.Core.Domain;

namespace Somi92.SkydivingLogbook.Core.Data.Configuration
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
