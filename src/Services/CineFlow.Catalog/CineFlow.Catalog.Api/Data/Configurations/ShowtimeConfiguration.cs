using CineFlow.Catalog.Api.Entities.Halls;
using CineFlow.Catalog.Api.Entities.Movies;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlow.Catalog.Api.Data.Configurations;

public sealed class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.ToTable("showtime");

        builder.HasKey(showtime => showtime.Id);

        builder.Property(showtime => showtime.Id)
            .HasColumnName("showtime_id")
            .ValueGeneratedNever();

        builder.Property(showtime => showtime.MovieId)
            .HasColumnName("movie_id")
            .IsRequired();

        builder.Property(showtime => showtime.HallId)
            .HasColumnName("hall_id")
            .IsRequired();

        builder.Property(showtime => showtime.StartTime)
            .HasColumnName("start_time")
            .IsRequired();

        builder.Property(showtime => showtime.EndTime)
            .HasColumnName("end_time")
            .IsRequired();

        builder.HasIndex(showtime => new { showtime.HallId, showtime.StartTime });

        builder.HasOne<Movie>()
            .WithMany()
            .HasForeignKey(showtime => showtime.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Hall>()
            .WithMany()
            .HasForeignKey(showtime => showtime.HallId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
