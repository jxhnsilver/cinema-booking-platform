using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.GetById;

public sealed record GetShowtimeByIdResponse(
    Guid Id,
    Guid MovieId,
    string MovieTitle,
    Guid HallId,
    string HallName,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);
