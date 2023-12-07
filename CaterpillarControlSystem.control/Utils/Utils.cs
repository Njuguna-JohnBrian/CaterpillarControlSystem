namespace CaterpillarControlSystem.control.Utils;

public abstract class Utils
{
    public const char UP = 'U';
    public const char DOWN = 'D';
    public const char LEFT = 'L';
    public const char RIGHT = 'R';

    public const char HEAD = 'H';
    public const char TAIL = 'T';
    public const char BODY = '0';

    public const char EMPTY = '.';
    public const char OBSTACLE = 'X';
    public const char SPICE = '$';
    public const char BOOSTER = 'B';

    public const int INITIAL_SIZE = 2;
    public const int MAX_SIZE = 5;
    public const int RADAR_SIZE = 11;
    public const int AREA_SIZE = 30;
    public const string LINEAR_GROWTH = "LinearGrowth";
}