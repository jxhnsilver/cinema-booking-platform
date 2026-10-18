using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Halls;

namespace CineFlow.Catalog.Api.Features.Halls.Create;

public sealed class CreateHallUseCase(CatalogDbContext context)
{
    public async Task<CreateHallResponse> ExecuteAsync(CreateHallRequest request, CancellationToken ct)
    {
        var defenition = new HallLayoutDefinition(
            request.Rows.Select(r => new HallRowDefinition(
                r.Row,
                r.Segments.Select(s => new SeatSegmentDefinition(
                    s.FirstSeatNumber,
                    s.SeatCount,
                    s.Category
                )).ToArray()
            )).ToArray()
        );

        var hall = Hall.Create(
            Guid.NewGuid(), 
            request.Name, 
            defenition);

        context.Halls.Add(hall);
        await context.SaveChangesAsync(ct);

        return new CreateHallResponse(
            hall.Id, 
            hall.Name, 
            hall.Seats.Count);
    }
}
