using CineFlow.Catalog.Api.Entities.Halls;

namespace CineFlow.Catalog.Api.Features.Halls.GetById;

public sealed record GetHallByIdResponse(
    Guid Id,
    string Name,
    int TotalSeats,
    HallLayoutResponse Layout);

public sealed record HallLayoutResponse(
    IReadOnlyCollection<HallRowResponse> Rows);

public sealed record HallRowResponse(
    int Row,
    IReadOnlyCollection<HallSeatResponse> Seats);

public sealed record HallSeatResponse(
    Guid Id,
    int Number,
    SeatCategory Category);
