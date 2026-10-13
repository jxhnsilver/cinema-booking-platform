using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Movies;

public sealed class Movie
{
    public Guid Id { get; init; }
    public string Title { get; private set; }
    public int DurationMinutes { get; private set; }

    public Movie(Guid id, string title, int durationMinutes)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new InvariantViolationException("Title cannot be null or empty.");

        if (durationMinutes <= 0)
            throw new InvariantViolationException("Duration must be a positive number.");

        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        Id = id;
        Title = title;
        DurationMinutes = durationMinutes;
    }
}
