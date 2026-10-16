namespace CineFlow.Catalog.Api.Features.Movies.Create;

public sealed record CreateMovieResponse(
    Guid Id,
    string Title,
    int DurationMinutes);