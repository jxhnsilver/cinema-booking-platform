using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Movies.GetAll;

public sealed class GetAllMoviesUseCase(CatalogDbContext context)
{
    public async Task<GetAllMoviesResponse> ExecuteAsync(CancellationToken ct)
    {
        var movieDtos =  await context.Movies
            .Select(m => new MovieListItemDto(m.Id, m.Title, m.DurationMinutes))
            .ToListAsync(ct);

        var response = new GetAllMoviesResponse(movieDtos);

        return response;
    }
}
