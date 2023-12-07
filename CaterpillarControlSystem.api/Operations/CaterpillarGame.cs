using CaterpillarControlSystem.api.Initializers;

namespace CaterpillarControlSystem.api.Operations;

public class CaterpillarGame
{
    private char _direction;
    private char[,] _area;
    private bool _mirror;
    private string _growthMode;
    private Stack<string> _history;
    private List<string> _log;


    public CaterpillarGame()
    {
        
        caterpillarSegments.Add(new CaterpillarSegment(InitialSizesAndPositions.CaterpillarInitialXPosition,
            InitialSizesAndPositions.CaterpillarInitialYPosition, InitCaterpillarSegment.Head));
        caterpillarSegments.Add(new CaterpillarSegment(InitialSizesAndPositions.CaterpillarInitialXPosition - 1,
            InitialSizesAndPositions.CaterpillarInitialYPosition, InitCaterpillarSegment.Body));
        caterpillarSegments.Add(new CaterpillarSegment(InitialSizesAndPositions.CaterpillarInitialXPosition - 2,
            InitialSizesAndPositions.CaterpillarInitialYPosition, InitCaterpillarSegment.Tail));
        _direction = InitRiderCommand.Right;
        _area = null;
        _mirror = false;
        _growthMode = InitCaterpillarGrowthModes.LinearGrowth;
        _history = new Stack<string>();
        _log = new List<string>();
    }

    public void Move(char direction)
    {
        Console.WriteLine(direction);
    }
}