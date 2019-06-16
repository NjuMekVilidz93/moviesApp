using Domain;
using EfDataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess
{
    public class moviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Role> Roles { get; set; }
       // public DbSet<MovieGenre> MovieGenres { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FSMAV3U\SQLEXPRESS;Initial Catalog=movies;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
            modelBuilder.ApplyConfiguration(new MovieReservationConfiguration());
            modelBuilder.ApplyConfiguration(new DirectorConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        //    modelBuilder.Entity<Director>().HasData(new { Id = 1, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Name = "Copola" });
        //    modelBuilder.Entity<Director>().HasData(new { Id = 2, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Name = "Tarantino" });
        //    modelBuilder.Entity<Movie>().HasData(new { Id = 1, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Title = "Harry Potter", DirectorId = 1, Description = "Opisno", AvailableCount = 4, Count = 12 });
        //    modelBuilder.Entity<Movie>().HasData(new { Id = 2, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Title = "The Lord of the Rings", DirectorId = 2, Description = "Opisnoo", AvailableCount = 2, Count = 7 });
        //    modelBuilder.Entity<Genre>().HasData(new { Id = 1, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Name = "Copola" });
        //    modelBuilder.Entity<Director>().HasData(new { Id = 1, CreatedAt = "2019-06-12 03:09:02.0677112|", IsDeleted = false, Name = "Copola" });
        }
    }
}
