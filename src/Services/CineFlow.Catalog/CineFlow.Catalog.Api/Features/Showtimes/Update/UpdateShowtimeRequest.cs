using System.ComponentModel.DataAnnotations;
namespace CineFlow.Catalog.Api.Features.Showtimes.Update;

public sealed record UpdateShowtimeRequest
{
    [Required]
    public Guid HallId { get; init; }

    [Required]
    public Guid MovieId { get; init; }

    [Required]
    public DateTime StartTime { get; init; }

}
