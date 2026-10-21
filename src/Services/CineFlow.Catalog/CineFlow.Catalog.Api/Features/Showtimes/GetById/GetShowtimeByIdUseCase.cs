using CineFlow.BuildingBlocks.Exceptions;
using CineFlow.Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CineFlow.Catalog.Api.Features.Showtimes.GetById;

public sealed class GetShowtimeByIdUseCase(CatalogDbContext context)
{
    public async Task<GetShowtimeByIdResponse> ExecuteAsync(
        Guid id,
        CancellationToken ct)
    {
        var showtime = await (
            from item in context.Showtimes
            join movie in context.Movies
                on item.MovieId equals movie.Id
            join hall in context.Halls
                on item.HallId equals hall.Id
            where item.Id == id
            select new GetShowtimeByIdResponse(
                item.Id,
                item.MovieId,
                movie.Title,
                item.HallId,
                hall.Name,
                item.StartTime,
                item.EndTime,
                item.Status))
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Showtime was not found.");

        return showtime;
    }
}
