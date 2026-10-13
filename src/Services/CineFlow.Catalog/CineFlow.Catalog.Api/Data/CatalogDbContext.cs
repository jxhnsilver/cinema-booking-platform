using CineFlow.Catalog.Api.Entities.Halls;
using CineFlow.Catalog.Api.Entities.Movies;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Data;

public sealed class CatalogDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}
