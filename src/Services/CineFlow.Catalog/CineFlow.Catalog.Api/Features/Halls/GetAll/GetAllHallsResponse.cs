namespace CineFlow.Catalog.Api.Features.Halls.GetAll;

public sealed record HallListItemResponse(
    Guid Id,
    string Name,
    int TotalSeats);

public sealed record GetAllHallsResponse(
    IReadOnlyCollection<HallListItemResponse> Halls);
