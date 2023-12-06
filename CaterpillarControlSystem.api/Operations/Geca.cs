using CaterpillarControlSystem.api.Initializers;

namespace CaterpillarControlSystem.api.Operations;

public class Geca
{
    private List<CaterpillarSegment> _caterpillarSegments;
    private char _direction;
    private char[,] _area;
    private bool _mirror;
    private string _growthMode;
    private Stack<string> _history;
    private List<string> _log;
    

    public Geca(int size, int xDirection, int yDirection)
    {
        _caterpillarSegments = new List<CaterpillarSegment>();
        _caterpillarSegments.Add(new CaterpillarSegment(xDirection, yDirection, InitCaterpillarSegment.Head));
        _caterpillarSegments.Add(new CaterpillarSegment(xDirection - 1, yDirection, InitCaterpillarSegment.Body));
        _caterpillarSegments.Add(new CaterpillarSegment(xDirection - size, yDirection, InitCaterpillarSegment.Tail));
        _direction = InitRiderCommand.Right;
        _area = null;
        _mirror = false;
        _growthMode = InitCaterpillarGrowthModes.LinearGrowth;
        _history = new Stack<string>();
        _log = new List<string>();


    }

   
}