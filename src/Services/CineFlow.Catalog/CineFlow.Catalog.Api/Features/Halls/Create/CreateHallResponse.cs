namespace CineFlow.Catalog.Api.Features.Halls.Create;

public sealed record CreateHallResponse(
    Guid Id,
    string Name,
    int TotalSeats);
