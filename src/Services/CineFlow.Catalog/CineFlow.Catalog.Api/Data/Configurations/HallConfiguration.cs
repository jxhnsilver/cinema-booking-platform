using CineFlow.Catalog.Api.Entities.Halls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlow.Catalog.Api.Data.Configurations;

public sealed class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.ToTable("halls");

        builder.HasKey(hall => hall.Id);

        builder.Property(hall => hall.Id)
            .HasColumnName("hall_id")
            .ValueGeneratedNever();

        builder.Property(hall => hall.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(hall => hall.Seats)
            .WithOne()
            .HasForeignKey(seat => seat.HallId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(hall => hall.Seats)
            .HasField("_seats")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
