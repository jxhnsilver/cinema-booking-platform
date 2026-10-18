using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Halls.GetAll;

public sealed class GetAllHallsUseCase(CatalogDbContext context)
{
    public async Task<GetAllHallsResponse> ExecuteAsync(CancellationToken ct)
    {
        var halls = await context.Halls
            .Select(hall => new HallListItemResponse(
                hall.Id,
                hall.Name,
                hall.Seats.Count))
            .ToListAsync(ct);

        return new GetAllHallsResponse(halls);
    }
}
