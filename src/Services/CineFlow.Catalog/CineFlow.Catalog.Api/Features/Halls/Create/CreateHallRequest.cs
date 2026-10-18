using CineFlow.Catalog.Api.Entities.Halls;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CineFlow.Catalog.Api.Features.Halls.Create;

public sealed record CreateHallRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Layout rows are required.")]
    [MinLength(1, ErrorMessage = "Hall must contain at least one row.")]
    public List<HallRowRequest> Rows { get; init; } = [];
}

public sealed record HallRowRequest
{
    [Range(1, 100, ErrorMessage = "Row number must be positive.")]
    public int Row { get; init; }

    [Required(ErrorMessage = "Row segments are required.")]
    [MinLength(1, ErrorMessage = "Row must contain at least one segment.")]
    public List<SeatSegmentRequest> Segments { get; init; } = [];
}

public sealed record SeatSegmentRequest
{
    [Range(1, 100, ErrorMessage = "First seat number must be positive.")]
    public int FirstSeatNumber { get; init; }

    [Range(1, 100, ErrorMessage = "Seat count must be positive.")]
    public int SeatCount { get; init; }

    [EnumDataType(typeof(SeatCategory), ErrorMessage = "Invalid seat category.")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SeatCategory Category { get; init; }
}