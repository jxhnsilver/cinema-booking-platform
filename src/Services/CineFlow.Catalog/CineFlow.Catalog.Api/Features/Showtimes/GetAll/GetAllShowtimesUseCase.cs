using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Entities.Showtimes;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Showtimes.GetAll;

public sealed class GetAllShowtimesUseCase(CatalogDbContext context)
{
    public async Task<GetAllShowtimesResponse> ExecuteAsync(
        DateTime? fromDate,
        ShowtimeStatus? status,
        CancellationToken ct)
    {
        var query =
            from showtime in context.Showtimes
            join movie in context.Movies
                on showtime.MovieId equals movie.Id
            join hall in context.Halls
                on showtime.HallId equals hall.Id
            select new
            {
                Showtime = showtime,
                MovieTitle = movie.Title,
                HallName = hall.Name
            };

        if (fromDate.HasValue)
        {
            var date = fromDate.Value.Date;
            query = query.Where(x => x.Showtime.StartTime >= date);
        }

        if (status.HasValue)
            query = query.Where(x => x.Showtime.Status == status.Value);

        var showtimes = await query
            .OrderBy(x => x.Showtime.StartTime)
            .Select(x => new ShowtimeListItemResponse(
                x.Showtime.Id,
                x.Showtime.MovieId,
                x.MovieTitle,
                x.Showtime.HallId,
                x.HallName,
                x.Showtime.StartTime,
                x.Showtime.EndTime,
                x.Showtime.Status))
            .ToListAsync(ct);

        return new GetAllShowtimesResponse(showtimes);
    }
}
