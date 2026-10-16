using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Movies.Delete;

public sealed class DeleteMovieUseCase(CatalogDbContext context)
{
    public async Task ExecuteAsync(Guid movieId, CancellationToken ct)
    {
        int rowsAffected = await context.Movies
            .Where(m => m.Id == movieId)
            .ExecuteDeleteAsync(ct);

        if (rowsAffected == 0)
            throw new NotFoundException($"Movie with id {movieId} not found.");
    }
}
