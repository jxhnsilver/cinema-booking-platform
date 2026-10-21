using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.GetAll;

public sealed record ShowtimeListItemResponse(
    Guid Id,
    Guid MovieId,
    string MovieTitle,
    Guid HallId,
    string HallName,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);

public sealed record GetAllShowtimesResponse(
    IReadOnlyCollection<ShowtimeListItemResponse> Showtimes);
