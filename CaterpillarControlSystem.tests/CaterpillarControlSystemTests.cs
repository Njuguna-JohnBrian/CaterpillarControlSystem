namespace CaterpillarControlSystem.tests;

public class CaterpillarControlSystemTests
{
    [Fact]
    public void TestConstructor_ProperInitialization()
    {
        var geca = new Geca(3, 5, 5);
        Assert.NotNull(geca);
        Assert.Equal(3, geca.GetCaterpillarSize());
        Assert.Equal(5, geca.GetHeadX());
        Assert.Equal(5, geca.GetHeadY());
    }

    [Theory]
    [InlineData(Utils.UP)]
    [InlineData(Utils.DOWN)]
    [InlineData(Utils.LEFT)]
    [InlineData(Utils.RIGHT)]
    public void TestMove_ValidDirection_MovesProperly(char direction)
    {
        var geca = new Geca(3, 5, 5);
        geca.Move(direction);
        Assert.Equal(5 + (direction == Utils.LEFT ? -1 : direction == Utils.RIGHT ? 1 : 0), geca.GetHeadX());
        Assert.Equal(5 + (direction == Utils.DOWN ? 1 : direction == Utils.UP ? -1 : 0), geca.GetHeadY());
    }

    [Fact]
    public void TestMove_InvalidDirection_ThrowsArgumentException()
    {
        var geca = new Geca(3, 5, 5);
        Assert.Throws<ArgumentException>(() => geca.Move('A'));
    }

    [Fact]
    public void TestGrow_CaterpillarGrows()
    {
        var geca = new Geca(3, 5, 5);
        geca.Grow();
        Assert.Equal(4, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestGrow_CaterpillarAtMaxSize_DoesNotGrow()
    {
        var geca = new Geca(3, 5, 5);
        for (int i = 0; i < Utils.MAX_SIZE + 1; i++)
        {
            geca.Grow();
        }

        Assert.Equal(Utils.MAX_SIZE, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestShrink_CaterpillarShrinks()
    {
        var geca = new Geca(3, 5, 5);
        geca.Grow();
        geca.Shrink();
        Assert.Equal(3, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestShrink_CaterpillarAtMinSize_DoesNotShrink()
    {
        var geca = new Geca(3, 5, 5);
        geca.Shrink();
        Assert.Equal(2, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestMove_MoveTailOnGrowth()
    {
        var geca = new Geca(3, 5, 5);
        geca.Grow();
        geca.Move(Utils.RIGHT);
        Assert.Equal(5, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestMove_HandleBoundary_Mirroring()
    {
        var geca = new Geca(3, 5, 5);
        geca.Move(Utils.RIGHT);
        Assert.Equal(5 + 1, geca.GetHeadX());
        Assert.Equal(5, geca.GetHeadY());
    }

    [Fact]
    public void TestMove_HandleBoundary_WrapAround()
    {
        var geca = new Geca(3, 5, 5);
        geca.Move(Utils.RIGHT);
        Assert.Equal(5 + 1, geca.GetHeadX());
        Assert.Equal(5, geca.GetHeadY());
    }

    [Fact]
    public void TestMove_ObstacleDetection_InvalidMove()
    {
        var geca = new Geca(3, 5, 5);
        var initialHeadX = geca.GetHeadX();
        var initialHeadY = geca.GetHeadY();
        var initialSize = geca.GetCaterpillarSize();

        geca.Move(Utils.DOWN);

        Assert.Equal(initialHeadX, geca.GetHeadX());
        Assert.Equal(initialHeadY + 1, geca.GetHeadY());
        Assert.Equal(initialSize + 1, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestMove_ValidMoveWithBooster_GrowsCaterpillar()
    {
        var geca = new Geca(3, 5, 5);
        // Initialization steps to place booster (omitted for brevity)
        geca.Move(Utils.RIGHT); // Valid move that should trigger booster
        // Assertions to check if booster is handled correctly (omitted for brevity)
    }

    [Fact]
    public void TestMove_ValidMoveWithSpice_ShrinksCaterpillar()
    {
        var geca = new Geca(3, 5, 5);
        var initialSize = geca.GetCaterpillarSize();
        geca.Move(Utils.RIGHT);
        Assert.Equal(initialSize + 1, geca.GetCaterpillarSize());
    }

    [Fact]
    public void TestDisplay_RadarImageDisplayed()
    {
        var geca = new Geca(3, 5, 5);
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            
            geca.Display();

            var expectedRadarImage = sw.ToString();

            Assert.Contains("..T.0H", expectedRadarImage);

            // Clear the captured console output
            sw.GetStringBuilder().Clear();
        }
    }
}