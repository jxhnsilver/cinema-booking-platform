using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Halls;

public sealed class Seat
{
    public Guid Id { get; init; }
    public Guid HallId { get; init; }
    public int Row { get; private set; }
    public int Number { get; private set; }
    public SeatCategory Category { get; private set; }

    private Seat() { }

    public Seat(Guid id, Guid hallId, int row, int number, SeatCategory category)
    {
        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        if (hallId == Guid.Empty)
            throw new InvariantViolationException("Hall ID cannot be empty.");

        if (row < 1 || number < 1)
            throw new InvariantViolationException(
                "Row and seat numbers must be positive.");

        Id = id;
        HallId = hallId;
        Row = row;
        Number = number;
        Category = category;
    }
}
