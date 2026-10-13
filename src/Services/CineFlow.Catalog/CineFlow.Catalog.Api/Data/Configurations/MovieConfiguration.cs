using CineFlow.Catalog.Api.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlow.Catalog.Api.Data.Configurations;

public sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movies");

        builder.HasKey(movie => movie.Id);

        builder.Property(movie => movie.Id)
            .ValueGeneratedNever();

        builder.Property(movie => movie.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(movie => movie.DurationMinutes)
            .HasColumnName("duration_minutes")
            .IsRequired();
    }
}
