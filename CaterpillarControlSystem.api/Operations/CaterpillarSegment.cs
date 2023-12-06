namespace CaterpillarControlSystem.api.Operations;

public class CaterpillarSegment
{
    public CaterpillarSegment(int xDirection, int yDirection, char segmentType)
    {
        this.XCoordinate = xDirection;
        this.YCoordinate = yDirection;
        this.SegmentType = segmentType;
    }

    public int XCoordinate { get; set; }

    public int YCoordinate { get; set; }


    public char SegmentType { get; set; }
}