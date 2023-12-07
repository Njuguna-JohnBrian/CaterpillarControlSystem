using CaterpillarControlSystem.control.model;

namespace CaterpillarControlSystem.control.Services;

public abstract class CommandService
{
    public char Direction { get; }
    public Segment Tail { get; }

    protected CommandService(char direction, Segment tail)
    {
        Direction = direction;
        Tail = tail;
    }
}