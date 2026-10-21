using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.Create;

public sealed record CreateShowtimeResponse(
    Guid Id,
    Guid MovieId,
    Guid HallId,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);
