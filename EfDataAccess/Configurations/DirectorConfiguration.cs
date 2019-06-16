using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(40).IsRequired();

            builder.Property(d => d.CreatedAt).HasDefaultValueSql("GETDATE()");

            
        }
    }
}
