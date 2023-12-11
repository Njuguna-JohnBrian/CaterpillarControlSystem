namespace CaterpillarControlSystem.tests
{
    public class LoggerServiceTests
{
    [Fact]
    public void LoggerService_Constructor_ShouldInitializeLogFiles()
    {
        var loggerService = new LoggerService();
        Assert.True(File.Exists(loggerService._errorLogPath));
        Assert.True(File.Exists(loggerService._movementLogPath));
        Assert.True(File.Exists(loggerService._generalLogPath));
    }

    [Fact]
    public void ClearLogs_ShouldEmptyAllLogFiles()
    {
        var loggerService = new LoggerService();
        loggerService.LogError("Error message");
        loggerService.LogMovement("Movement message");
        loggerService.LogGeneral("General message");

        loggerService.ClearLogs();

        Assert.Equal(string.Empty, File.ReadAllText(loggerService._errorLogPath));
        Assert.Equal(string.Empty, File.ReadAllText(loggerService._movementLogPath));
        Assert.Equal(string.Empty, File.ReadAllText(loggerService._generalLogPath));
    }

    [Fact]
    public void LogError_ShouldAppendErrorMessageToLogFile()
    {
        var loggerService = new LoggerService();
        const string errorMessage = "Test error";
        loggerService.LogError(errorMessage);

        var lastEntry = File.ReadLines(loggerService._errorLogPath).Last();
        Assert.Contains(errorMessage, lastEntry);
    }

    [Fact]
    public void LogMovement_ShouldAppendMovementMessageToLogFile()
    {
        var loggerService = new LoggerService();
        const string movementMessage = "Test movement";
        loggerService.LogMovement(movementMessage);

        var lastEntry = File.ReadLines(loggerService._movementLogPath).Last();
        Assert.Contains(movementMessage, lastEntry);
    }

    [Fact]
    public void LogGeneral_ShouldAppendGeneralMessageToLogFile()
    {
        var loggerService = new LoggerService();
        const string generalMessage = "Test general";
        loggerService.LogGeneral(generalMessage);

        var lastEntry = File.ReadLines(loggerService._generalLogPath).Last();
        Assert.Contains(generalMessage, lastEntry);
    }
}
}

