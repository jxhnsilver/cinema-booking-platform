using System.ComponentModel.DataAnnotations;

namespace CineFlow.Catalog.Api.Features.Movies.Create;

public sealed record CreateMovieRequest
{
    [Required(ErrorMessage = "Title is required and cannot be null or empty.")]
    [StringLength(250, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 250 characters long.")]
    public string Title { get; init; } = string.Empty;

    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
    public int DurationMinutes { get; init; }
}
