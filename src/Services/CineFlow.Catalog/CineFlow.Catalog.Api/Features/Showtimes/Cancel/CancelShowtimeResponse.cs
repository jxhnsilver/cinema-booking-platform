using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.Cancel;

public sealed record CancelShowtimeResponse(
    Guid Id,
    Guid MovieId,
    Guid HallId,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);
