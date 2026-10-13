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
            .ValueGeneratedNever();

        builder.Property(hall => hall.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
