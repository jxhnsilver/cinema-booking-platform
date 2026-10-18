using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Halls;

public sealed class Hall
{
    private readonly List<Seat> _seats = [];

    public Guid Id { get; init; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Seat> Seats => _seats.AsReadOnly();

    private Hall() { }

    private Hall(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Hall Create(Guid id, string name, HallLayoutDefinition definition)
    {
        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new InvariantViolationException("Name cannot be null or empty.");

        if (definition == null)
            throw new InvariantViolationException("Hall layout definition cannot be null.");

        var hall = new Hall(id, name);
        hall._seats.AddRange(GenerateSeats(hall.Id, definition));

        return hall;
    }

    private static List<Seat> GenerateSeats(Guid hallId, HallLayoutDefinition definition)
    {
        if (definition.Rows.Count == 0)
            throw new InvariantViolationException("A hall layout must contain at least one row.");

        var capacity = definition.Rows.Sum(r => r.Segments.Sum(s => s.SeatCount));
        var seats = new List<Seat>(capacity);
        var usedRows = new HashSet<int>();

        foreach (var row in definition.Rows)
        {
            if (row.RowNumber < 1)
                throw new InvariantViolationException("Row number must be positive.");

            if (!usedRows.Add(row.RowNumber))
                throw new InvariantViolationException("Row numbers must be unique.");

            if (row.Segments.Count == 0)
                throw new InvariantViolationException("Every row must have at least one seat segment.");

            var usedSeatNumbers = new HashSet<int>();

            foreach (var segment in row.Segments)
            {
                if (segment.FirstSeatNumber < 1 || segment.SeatCount < 1)
                    throw new InvariantViolationException(
                        "First seat number and segment size must be positive.");

                if (!Enum.IsDefined(segment.Category))
                    throw new InvariantViolationException("Unknown seat category.");

                for (var i = 0; i < segment.SeatCount; i++)
                {
                    var seatNumber = segment.FirstSeatNumber + i;

                    if (!usedSeatNumbers.Add(seatNumber))
                        throw new InvariantViolationException(
                            "Seat segments in a row cannot overlap.");

                    seats.Add(new Seat(
                        Guid.NewGuid(),
                        hallId,
                        row.RowNumber,
                        seatNumber,
                        segment.Category));
                }
            }
        }

        if (seats.Count == 0)
            throw new InvariantViolationException("A hall must have at least one seat.");

        return seats;
    }
}
