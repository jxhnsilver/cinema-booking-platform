using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Validators.Showtimes;

public static class HallScheduleConflictValidator
{
    private const int CleanupBufferMinutes = 15;

    public static async Task EnsureNewShowtimeFitsHallScheduleAsync(
        CatalogDbContext context,
        Showtime showtime,
        CancellationToken ct)
    {
        var hasConflict = await context.Showtimes.AnyAsync(x =>
            x.HallId == showtime.HallId &&
            x.Status != ShowtimeStatus.Cancelled &&
            x.StartTime < showtime.EndTime.AddMinutes(CleanupBufferMinutes) &&
            x.EndTime.AddMinutes(CleanupBufferMinutes) > showtime.StartTime,
            ct);

        ThrowIfConflict(hasConflict);
    }

    public static async Task EnsureRescheduledShowtimeFitsHallScheduleAsync(
        CatalogDbContext context,
        Showtime showtime,
        CancellationToken ct)
    {
        var hasConflict = await context.Showtimes.AnyAsync(x =>
            x.Id != showtime.Id &&
            x.HallId == showtime.HallId &&
            x.Status != ShowtimeStatus.Cancelled &&
            x.StartTime < showtime.EndTime.AddMinutes(CleanupBufferMinutes) &&
            x.EndTime.AddMinutes(CleanupBufferMinutes) > showtime.StartTime,
            ct);

        ThrowIfConflict(hasConflict);
    }

    private static void ThrowIfConflict(bool hasConflict)
    {
        if (hasConflict)
            throw new BusinessRuleException(
                "The hall is occupied during this time or does not have enough turnaround time.");
    }
}
