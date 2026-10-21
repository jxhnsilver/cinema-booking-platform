using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Workers;

public sealed class ShowtimeStartWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<ShowtimeStartWorker> logger) : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var timer = new PeriodicTimer(PollInterval);

        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
                var now = DateTime.UtcNow;

                var showtimes = await context.Showtimes
                    .Where(x => x.Status == ShowtimeStatus.Scheduled &&
                                x.StartTime <= now)
                    .ToListAsync(ct);

                foreach (var showtime in showtimes)
                    showtime.Start();

                if (showtimes.Count > 0)
                    await context.SaveChangesAsync(ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to start showtimes.");
            }
        }
    }
}
