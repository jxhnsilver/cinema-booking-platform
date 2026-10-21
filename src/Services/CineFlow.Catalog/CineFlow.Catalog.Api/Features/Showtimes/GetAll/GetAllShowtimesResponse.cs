using CineFlow.Catalog.Api.Entities.Showtimes;

namespace CineFlow.Catalog.Api.Features.Showtimes.GetAll;

/// <summary>Represents a showtime returned in a list.</summary>
public sealed record ShowtimeListItemResponse(
    Guid Id,
    Guid MovieId,
    string MovieTitle,
    Guid HallId,
    string HallName,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);

/// <summary>Contains showtimes matching the requested filters.</summary>
public sealed record GetAllShowtimesResponse(
    IReadOnlyCollection<ShowtimeListItemResponse> Showtimes);
