using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Domain.Halls;

public sealed class Seat
{
    public Guid Id { get; init; }
    public Guid HallId { get; init; }
    public int Row { get; private set; }
    public int Number { get; private set; }
    public SeatType Type { get; private set; }

    public Seat(Guid id, Guid hallId, int row, int number, SeatType type)
    {
        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        if (hallId == Guid.Empty)
            throw new InvariantViolationException("Hall ID cannot be empty.");

        if (row < 0)
            throw new InvariantViolationException("Row must be a non-negative number.");

        if (number < 0)
            throw new InvariantViolationException("Number must be a non-negative number.");

        Id = id;
        HallId = hallId;
        Row = row;
        Number = number;
        Type = type;
    }
}
