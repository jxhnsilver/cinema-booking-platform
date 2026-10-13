using CineFlow.BuildingBlocks.Exceptions;

namespace CineFlow.Catalog.Api.Entities.Halls;

public sealed class Hall
{
    public Guid Id { get; init; }
    public string Name { get; private set; }

    public Hall(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new InvariantViolationException("ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(name))
            throw new InvariantViolationException("Name cannot be null or empty.");

        Id = id;
        Name = name;
    }
}
