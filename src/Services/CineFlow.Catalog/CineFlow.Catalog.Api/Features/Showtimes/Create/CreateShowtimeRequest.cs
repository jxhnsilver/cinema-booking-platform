using System.ComponentModel.DataAnnotations;

namespace CineFlow.Catalog.Api.Features.Showtimes.Create;

public sealed record CreateShowtimeRequest
{
    [Required] public Guid HallId { get; init; }
    [Required] public Guid MovieId { get; init; }
    [Required] public DateTime StartTime { get; init; }
}
