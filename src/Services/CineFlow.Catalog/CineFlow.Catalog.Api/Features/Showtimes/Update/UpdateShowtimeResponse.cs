using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.Update;

public sealed record UpdateShowtimeResponse(
    Guid Id,
    Guid MovieId,
    Guid HallId,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);
