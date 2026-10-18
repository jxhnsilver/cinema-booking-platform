namespace CineFlow.Catalog.Api.Entities.Halls;

public sealed record HallLayoutDefinition(
    IReadOnlyCollection<HallRowDefinition> Rows);

public sealed record HallRowDefinition(
    int RowNumber,
    IReadOnlyCollection<SeatSegmentDefinition> Segments);

public sealed record SeatSegmentDefinition(
    int FirstSeatNumber,
    int SeatCount,
    SeatCategory Category);
