using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Movies;

namespace CineFlow.Catalog.Api.Features.Movies.Create;

public sealed class CreateMovieUseCase(CatalogDbContext context)
{
    public async Task<CreateMovieResponse> ExecuteAsync(
        CreateMovieRequest request, 
        CancellationToken ct)
    {
        var movieId = Guid.NewGuid();
        var movie = new Movie(movieId, request.Title, request.DurationMinutes);

        context.Movies.Add(movie);
        await context.SaveChangesAsync(ct);

        var response = new CreateMovieResponse(
            movie.Id, 
            movie.Title, 
            movie.DurationMinutes);

        return response;
    }
}
