using CineFlow.Catalog.Api.Entities.Halls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlow.Catalog.Api.Data.Configurations;

public sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("seats");

        builder.HasKey(seat => seat.Id);

        builder.Property(seat => seat.Id)
            .HasColumnName("seat_id")
            .ValueGeneratedNever();

        builder.Property(seat => seat.HallId)
            .HasColumnName("hall_id")
            .IsRequired();

        builder.Property(seat => seat.Row)
            .HasColumnName("row")
            .IsRequired();

        builder.Property(seat => seat.Number)
            .HasColumnName("number")
            .IsRequired();

        builder.Property(seat => seat.Category)
            .HasColumnName("category")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(seat => new { seat.HallId, seat.Row, seat.Number })
            .IsUnique();
    }
}
