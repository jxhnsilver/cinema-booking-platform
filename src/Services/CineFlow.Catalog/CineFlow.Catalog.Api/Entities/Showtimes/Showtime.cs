using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Showtimes;

public sealed class Showtime
{
    public Guid Id { get; init; }
    public Guid MovieId { get; init; }
    public Guid HallId { get; init; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public Showtime(Guid id, Guid movieId, Guid hallId, DateTime startTime, DateTime endTime)
    {
        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        if (movieId == Guid.Empty)
            throw new InvariantViolationException("Movie ID cannot be empty.");

        if (hallId == Guid.Empty)
            throw new InvariantViolationException("Hall ID cannot be empty.");

        if (startTime <= DateTime.MinValue)
            throw new InvariantViolationException("Start time must be a valid date and time.");

        if (endTime <= startTime)
            throw new InvariantViolationException("End time must be greater than start time.");

        Id = id;
        MovieId = movieId;
        HallId = hallId;
        StartTime = startTime;
        EndTime = endTime;
    }
}
