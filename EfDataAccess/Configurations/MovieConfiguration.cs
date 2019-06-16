using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title).HasMaxLength(35).IsRequired();

            builder.HasIndex(m => m.Title).IsUnique();

            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(m => m.MovieGenres).WithOne(mg => mg.Movie).HasForeignKey(mg => mg.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.ReservationMovies).WithOne(mr => mr.Movie).HasForeignKey(mr => mr.MovieId).OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
