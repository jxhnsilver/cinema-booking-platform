using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Movies.GetById;

public sealed class GetMovieByIdUseCase(CatalogDbContext context)
{
    public async Task<GetMovieByIdResponse> ExecuteAsync(Guid movieId, CancellationToken ct)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie is null)
            throw new NotFoundException($"Movie with id {movieId} not found.");

        var response = new GetMovieByIdResponse(
            movie.Id, 
            movie.Title, 
            movie.DurationMinutes);

        return response;
    }
}
