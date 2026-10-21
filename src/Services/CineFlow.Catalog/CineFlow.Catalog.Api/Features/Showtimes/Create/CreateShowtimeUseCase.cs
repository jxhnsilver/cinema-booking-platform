using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Showtimes;
using CineFlow.Catalog.Api.Validators.Showtimes;
using CineFlow.BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Showtimes.Create;

public sealed class CreateShowtimeUseCase(CatalogDbContext context)
{
    public async Task<CreateShowtimeResponse> ExecuteAsync(
        CreateShowtimeRequest request,
        CancellationToken ct)
    {
        var duration = await context.Movies
            .Where(x => x.Id == request.MovieId)
            .Select(x => (int?)x.DurationMinutes)
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Movie was not found.");

        if (!await context.Halls.AnyAsync(x => x.Id == request.HallId, ct))
            throw new NotFoundException("Hall was not found.");

        var showtime = Showtime.Create(
            Guid.NewGuid(),
            request.MovieId,
            request.HallId,
            request.StartTime,
            request.StartTime.AddMinutes(duration));

        await HallScheduleConflictValidator.EnsureNewShowtimeFitsHallScheduleAsync(
            context, showtime, ct);

        context.Showtimes.Add(showtime);
        await context.SaveChangesAsync(ct);

        return new CreateShowtimeResponse(
            showtime.Id, 
            showtime.MovieId, 
            showtime.HallId,
            showtime.StartTime, 
            showtime.EndTime, 
            showtime.Status);
    }
}
