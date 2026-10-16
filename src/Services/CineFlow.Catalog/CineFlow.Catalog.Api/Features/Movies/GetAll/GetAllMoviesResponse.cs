namespace CineFlow.Catalog.Api.Features.Movies.GetAll;

public sealed record MovieListItemDto(
    Guid Id,
    string Title,
    int DurationMinutes);

public sealed record GetAllMoviesResponse(IReadOnlyCollection<MovieListItemDto> Movies);
