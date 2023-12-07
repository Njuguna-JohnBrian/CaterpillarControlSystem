namespace CaterpillarControlSystem.control.Services;

public class LoggerService
{
    private static readonly string ProjectRoot =
        Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

    private static string LogsFolder => Path.Combine(ProjectRoot, "logs");

    private readonly string _errorLogPath = Path.Combine(LogsFolder, "error.txt");
    private readonly string _movementLogPath = Path.Combine(LogsFolder, "movement.txt");
    private readonly string _generalLogPath = Path.Combine(LogsFolder, "general.txt");


    public LoggerService()
    {
        InitializeLogFile(_errorLogPath);
        InitializeLogFile(_movementLogPath);
        InitializeLogFile(_generalLogPath);
    }

    private void InitializeLogFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

            File.Create(filePath).Close();
        }
    }

    public void ClearLogs()
    {
        ClearLogFile(_errorLogPath);
        ClearLogFile(_movementLogPath);
        ClearLogFile(_generalLogPath);
    }

    private static void ClearLogFile(string filePath)
    {
        File.WriteAllText(filePath, string.Empty);
    }

    public void LogError(string errorMessage)
    {
        LogToFile(_errorLogPath, errorMessage);
    }

    public void LogMovement(string movementMessage)
    {
        LogToFile(_movementLogPath, movementMessage);
    }

    public void LogGeneral(string generalMessage)
    {
        LogToFile(_generalLogPath, generalMessage);
    }

    private static void LogToFile(string filePath, string message)
    {
        using var writer = new StreamWriter(filePath, true);
        writer.Write($"\n{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }
}