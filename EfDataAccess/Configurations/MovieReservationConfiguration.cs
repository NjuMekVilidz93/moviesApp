using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class MovieReservationConfiguration : IEntityTypeConfiguration<MovieReservation>
    {
        public void Configure(EntityTypeBuilder<MovieReservation> builder)
        {
            builder.HasKey(mr => new { mr.MovieId, mr.ReservationId });
        }
    }
}
