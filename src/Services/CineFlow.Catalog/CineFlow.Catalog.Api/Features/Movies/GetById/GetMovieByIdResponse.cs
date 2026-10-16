namespace CineFlow.Catalog.Api.Features.Movies.GetById;

public sealed record GetMovieByIdResponse(
    Guid Id,
    string Title,
    int DurationMinutes);
