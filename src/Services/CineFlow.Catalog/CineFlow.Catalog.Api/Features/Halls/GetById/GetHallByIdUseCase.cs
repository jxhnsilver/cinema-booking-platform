using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Halls.GetById;

public sealed class GetHallByIdUseCase(CatalogDbContext context)
{
    public async Task<GetHallByIdResponse> ExecuteAsync(Guid hallId, CancellationToken ct)
    {
        var hall = await context.Halls
            .Include(h => h.Seats)
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == hallId, ct);

        if (hall is null)
            throw new NotFoundException($"Hall with id {hallId} not found.");

        var rows = hall.Seats
            .GroupBy(seat => seat.Row)
            .OrderBy(row => row.Key)
            .Select(row => new HallRowResponse(
                row.Key,
                row
                    .OrderBy(seat => seat.Number)
                    .Select(seat => new HallSeatResponse(
                        seat.Id,
                        seat.Number,
                        seat.Category))
                    .ToArray()))
            .ToArray();

        return new GetHallByIdResponse(
            hall.Id,
            hall.Name,
            hall.Seats.Count,
            new HallLayoutResponse(rows));
    }
}
