using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Showtimes.Cancel;

public sealed class CancelShowtimeUseCase(CatalogDbContext context)
{
    public async Task<CancelShowtimeResponse> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var showtime = await context.Showtimes
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Showtime was not found.");

        showtime.Cancel();
        await context.SaveChangesAsync(ct);

        return new CancelShowtimeResponse(
            showtime.Id, showtime.MovieId, showtime.HallId,
            showtime.StartTime, showtime.EndTime, showtime.Status);
    }
}

public sealed record CancelShowtimeResponse(
    Guid Id,
    Guid MovieId,
    Guid HallId,
    DateTime StartTime,
    DateTime EndTime,
    ShowtimeStatus Status);
