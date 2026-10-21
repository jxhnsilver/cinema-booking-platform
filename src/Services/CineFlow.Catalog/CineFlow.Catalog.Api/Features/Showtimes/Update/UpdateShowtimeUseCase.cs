using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Validators.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Showtimes.Update;

public sealed class UpdateShowtimeUseCase(CatalogDbContext context)
{
    public async Task<UpdateShowtimeResponse> ExecuteAsync(
        Guid id,
        UpdateShowtimeRequest request,
        CancellationToken ct)
    {
        var showtime = await context.Showtimes
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Showtime was not found.");

        var duration = await context.Movies
            .Where(x => x.Id == request.MovieId)
            .Select(x => (int?)x.DurationMinutes)
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Movie was not found.");

        if (!await context.Halls.AnyAsync(x => x.Id == request.HallId, ct))
            throw new NotFoundException("Hall was not found.");

        var endTime = request.StartTime.AddMinutes(duration);
        showtime.UpdateSchedule(
            request.MovieId,
            request.HallId,
            request.StartTime,
            endTime);

        await HallScheduleConflictValidator.EnsureRescheduledShowtimeFitsHallScheduleAsync(
            context, showtime, ct);

        await context.SaveChangesAsync(ct);
        return new UpdateShowtimeResponse(
            showtime.Id, 
            showtime.MovieId, 
            showtime.HallId,
            showtime.StartTime, 
            showtime.EndTime, 
            showtime.Status);
    }
}
