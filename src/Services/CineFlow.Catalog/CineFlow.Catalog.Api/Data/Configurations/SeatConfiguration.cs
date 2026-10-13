using CineFlow.Catalog.Api.Entities.Halls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlow.Catalog.Api.Data.Configurations;

public sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("hall_seats");

        builder.HasKey(seat => seat.Id);

        builder.Property(seat => seat.Id)
            .ValueGeneratedNever();

        builder.Property(seat => seat.HallId)
            .HasColumnName("hall_id")
            .IsRequired();

        builder.Property(seat => seat.Row)
            .HasColumnName("row_number")
            .IsRequired();

        builder.Property(seat => seat.Number)
            .HasColumnName("seat_number")
            .IsRequired();

        builder.Property(seat => seat.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(seat => new { seat.HallId, seat.Row, seat.Number })
            .IsUnique();

        builder.HasOne<Hall>()
            .WithMany()
            .HasForeignKey(seat => seat.HallId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
