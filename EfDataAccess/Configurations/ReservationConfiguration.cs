using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(r => r.MovieReservations).WithOne(mr => mr.Reservation).HasForeignKey(mr => mr.ReservationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
