using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Showtimes;

public sealed class Showtime
{
    public Guid Id { get; init; }
    public Guid MovieId { get; private set; }
    public Guid HallId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public ShowtimeStatus Status { get; private set; }

    private Showtime() { }

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
        Status = ShowtimeStatus.Scheduled;
    }

    public static Showtime Create(
        Guid movieId,
        Guid hallId,
        DateTime startTime,
        DateTime endTime)
    {
        return new Showtime(Guid.NewGuid(), movieId, hallId, startTime, endTime);
    }

    public void UpdateSchedule(
        Guid movieId,
        Guid hallId,
        DateTime startTime,
        DateTime endTime)
    {
        if (movieId == Guid.Empty)
            throw new InvariantViolationException("Movie ID cannot be empty.");

        if (hallId == Guid.Empty)
            throw new InvariantViolationException("Hall ID cannot be empty.");

        if (startTime <= DateTime.MinValue)
            throw new InvariantViolationException("Start time must be a valid date and time.");

        if (endTime <= startTime)
            throw new InvariantViolationException("End time must be greater than start time.");

        MovieId = movieId;
        HallId = hallId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void Start()
    {
        ChangeStatus(ShowtimeStatus.InProgress);
    }

    public void Complete()
    {
        ChangeStatus(ShowtimeStatus.Completed);
    }

    public void Cancel()
    {
        ChangeStatus(ShowtimeStatus.Cancelled);
    }

    private void ChangeStatus(ShowtimeStatus targetStatus)
    {
        var isValidChange = (Status, targetStatus) switch
        {
            (ShowtimeStatus.Scheduled, ShowtimeStatus.InProgress) => true,
            (ShowtimeStatus.Scheduled, ShowtimeStatus.Cancelled) => true,
            (ShowtimeStatus.InProgress, ShowtimeStatus.Completed) => true,
            (ShowtimeStatus.InProgress, ShowtimeStatus.Cancelled) => true,
            _ => false
        };

        if (!isValidChange)
            throw new BusinessRuleException(
                $"Cannot change showtime status from {Status} to {targetStatus}.");

        Status = targetStatus;
    }
}
