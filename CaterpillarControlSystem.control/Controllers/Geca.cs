using CaterpillarControlSystem.control.Operations;
using CaterpillarControlSystem.control.Services;

namespace CaterpillarControlSystem.control.model;

public class Geca
{
    private readonly List<Segment> _caterpillar;
    private char[,]? _area;
    private int _headIndex;
    private bool _mirror;
    private string _growthMode;
    private Stack<CommandService> _history;
    private readonly List<string> _log;


    public Geca(int size, int x, int y)
    {
        _caterpillar = new List<Segment>();
        _caterpillar.Add(new Segment(x, y, Utils.Utils.HEAD));
        _caterpillar.Add(
            new Segment(x - 1, y, Utils.Utils.BODY));
        _caterpillar.Add(new Segment(x - size, y,
            Utils.Utils.TAIL));
        _headIndex = 0;
        _mirror = false;
        _growthMode = Utils.Utils.LINEAR_GROWTH;
        _history = new Stack<CommandService>();
        _log = new List<string>();
    }

    public void Move(char direction)
    {
        ValidateDirection(direction);

        var newX = _caterpillar[_headIndex].X;
        var newY = _caterpillar[_headIndex].Y;

        switch (direction)
        {
            case Utils.Utils.UP:
                newY--;
                break;
            case Utils.Utils.DOWN:
                newY++;
                break;
            case Utils.Utils.LEFT:
                newX--;
                break;
            case Utils.Utils.RIGHT:
                newX++;
                break;
        }


        HandleBoundary(ref newX, ref newY);

        var newHead = new Segment(newX, newY, Utils.Utils.HEAD);
        _caterpillar.Insert(0, newHead);

        if (_caterpillar.Count > Utils.Utils.MAX_SIZE)
        {
            _caterpillar.RemoveAt(_caterpillar.Count - 1);
        }

        _headIndex = 0;


        UpdateTail();


        CheckForSpiceAndBoosters(newHead);

        LogCommand($"Moved {direction}");
    }

    private void UpdateTail()
    {
        var head = _caterpillar[0];
        var tail = _caterpillar[^1];


        if (Math.Abs(head.X - tail.X) == 2 && head.Y == tail.Y)
        {
            MoveTail(head.X > tail.X ? Utils.Utils.LEFT : Utils.Utils.RIGHT);
        }
        else if (Math.Abs(head.Y - tail.Y) == 2 && head.X == tail.X)
        {
            MoveTail(head.Y > tail.Y ? Utils.Utils.UP : Utils.Utils.DOWN);
        }

        else if (!IsAdjacent(head, tail) && head.X != tail.X && head.Y != tail.Y)
        {
            MoveTailDiagonally(head, tail);
        }
    }

    private void MoveTail(char direction)
    {
        var tail = _caterpillar[^1];

        switch (direction)
        {
            case Utils.Utils.UP:
                _caterpillar.Add(new Segment(tail.X, tail.Y - 1, Utils.Utils.TAIL));
                break;
            case Utils.Utils.DOWN:
                _caterpillar.Add(new Segment(tail.X, tail.Y + 1, Utils.Utils.TAIL));
                break;
            case Utils.Utils.LEFT:
                _caterpillar.Add(new Segment(tail.X - 1, tail.Y, Utils.Utils.TAIL));
                break;
            case Utils.Utils.RIGHT:
                _caterpillar.Add(new Segment(tail.X + 1, tail.Y, Utils.Utils.TAIL));
                break;
        }

        _caterpillar.RemoveAt(_caterpillar.Count - 1);
    }

    private void MoveTailDiagonally(Segment head, Segment tail)
    {
        var xDiff = head.X - tail.X;
        var yDiff = head.Y - tail.Y;

        if (xDiff > 0)
        {
            MoveTail(yDiff > 0 ? Utils.Utils.UP : Utils.Utils.DOWN);
        }
        else
        {
            MoveTail(yDiff > 0 ? Utils.Utils.LEFT : Utils.Utils.RIGHT);
        }
    }

    private static bool IsAdjacent(Segment seg1, Segment seg2)
    {
        return Math.Abs(seg1.X - seg2.X) <= 1 && Math.Abs(seg1.Y - seg2.Y) <= 1;
    }

    public void Grow()
    {
        if (_caterpillar.Count < Utils.Utils.MAX_SIZE)
        {
            var tail = _caterpillar.Last();
            var newTail = new Segment(tail.X - 1, tail.Y,
                Utils.Utils.TAIL);
            _caterpillar.Add(newTail);
            tail.Type = Utils.Utils.BODY;
            LogCommand("Grew Caterpillar");
        }
        else
        {
            Console.WriteLine("Caterpillar is already at maximum size.");
        }

        LogCommand("Grew Caterpillar");
    }

    public void Shrink()
    {
        if (_caterpillar.Count > 1)
        {
            var tail = _caterpillar.Last();
            _caterpillar.Remove(tail);
            _caterpillar.Last().Type = Utils.Utils.TAIL;
            LogCommand("Shrank Caterpillar");
        }
        else
        {
            Console.WriteLine("Caterpillar is at minimum size.");
        }

        LogCommand("Shrank Caterpillar");
    }

    public void Display()
    {
        var display = new char[Utils.Utils.RADAR_SIZE,
            Utils.Utils.RADAR_SIZE];
        var xOffset = _caterpillar[_headIndex].X -
                      (Utils.Utils.RADAR_SIZE / 2);
        var yOffset = _caterpillar[_headIndex].Y -
                      (Utils.Utils.RADAR_SIZE / 2);

        for (var i = 0; i < Utils.Utils.RADAR_SIZE; i++)
        {
            for (var j = 0; j < Utils.Utils.RADAR_SIZE; j++)
            {
                var x = xOffset + j;
                var y = yOffset + i;
                display[i, j] = GetElement(x, y);
            }
        }

        foreach (var segment in _caterpillar)
        {
            var x = segment.X - xOffset;
            var y = segment.Y - yOffset;

            if (x >= 0 && x < Utils.Utils.RADAR_SIZE &&
                y >= 0 && y < Utils.Utils.RADAR_SIZE)
            {
                display[y, x] = segment.Type;
            }
        }

        for (var i = 0; i < Utils.Utils.RADAR_SIZE; i++)
        {
            for (var j = 0; j < Utils.Utils.RADAR_SIZE; j++)
            {
                Console.Write(display[i, j]);
            }

            Console.WriteLine();
        }

        LogCommand("Displayed Radar Image");
    }

    private void HandleBoundary(ref int x, ref int y)
    {
        if (_area != null)
        {
            if (_mirror)
            {
                x = MirrorCoordinate(x, _area.GetLength(1));
                y = MirrorCoordinate(y, _area.GetLength(0));
            }
            else
            {
                x = (x - _area.GetLength(1)) % _area.GetLength(1);
                y = (y - _area.GetLength(0)) % _area.GetLength(0);

                if (x < 0)
                {
                    x += _area.GetLength(1);
                }

                if (y < 0)
                {
                    y += _area.GetLength(0);
                }
            }
        }
    }

    private static int MirrorCoordinate(int coordinate, int boundary)
    {
        if (coordinate < 0)
        {
            return boundary + coordinate;
        }
        else if (coordinate >= boundary)
        {
            return boundary - (coordinate - boundary) - 1;
        }

        return coordinate;
    }

    private void CheckForSpiceAndBoosters(Segment head)
    {
        var element = GetElement(head.X, head.Y);

        switch (element)
        {
            case Utils.Utils.BOOSTER:
                Grow();
                break;
            case Utils.Utils.SPICE:
                Shrink();
                break;
        }
    }

    private bool IsValidMove(int x, int y)
    {
        var element = GetElement(x, y);

        return element != Utils.Utils.OBSTACLE &&
               !_caterpillar.Any(segment => segment.X == x && segment.Y == y);
    }

    private char GetElement(int x, int y)
    {
        if (_area != null)
        {
            var relativeX = x;
            var relativeY = y;

            if (_mirror)
            {
                relativeX = MirrorCoordinate(x, _area.GetLength(1));
                relativeY = MirrorCoordinate(y, _area.GetLength(0));
            }
            else
            {
                relativeX = (x - _area.GetLength(1)) % _area.GetLength(1);
                relativeY = (y - _area.GetLength(0)) % _area.GetLength(0);

                if (relativeX < 0)
                {
                    relativeX += _area.GetLength(1);
                }

                if (relativeY < 0)
                {
                    relativeY += _area.GetLength(0);
                }
            }

            return _area[relativeY, relativeX];
        }
        else
        {
            return Utils.Utils.EMPTY;
        }
    }

    private void LogCommand(string action)
    {
        _log.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {action}");
    }

    private static void ValidateDirection(char direction)
    {
        if (new char[]
            {
                Utils.Utils.UP,
                Utils.Utils.DOWN,
                Utils.Utils.LEFT,
                Utils.Utils.RIGHT
            }.Contains(direction))
        {
            return;
        }

        throw new ArgumentException("Invalid direction. Please enter U, D, L, or R.");
    }

    public int GetCaterpillarSize()
    {
        return _caterpillar.Count;
    }
    public int GetHeadX()
    {
        return _caterpillar[_headIndex].X;
    }

    public int GetHeadY()
    {
        return _caterpillar[_headIndex].Y;
    }
}