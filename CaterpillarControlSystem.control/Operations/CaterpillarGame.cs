using CaterpillarControlSystem.control.model;
using CaterpillarControlSystem.control.Services;

namespace CaterpillarControlSystem.control.Operations;

public class CaterpillarControlSystem
{
    private readonly Geca _geca = new(Utils.Utils.RADAR_SIZE, Utils.Utils.INITIAL_SIZE, Utils.Utils.INITIAL_SIZE);

    private readonly LoggerService _logger = new LoggerService();


    public void Run()
    {
        while (true)
        {
            Console.Clear();
            DisplayMenu();
            var command = Console.ReadLine()?.ToUpper();

            try
            {
                if (command != null) ProcessCommand(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Enter command (U/D/L/R/G/S/Q):");
        Console.WriteLine("U - Move Up");
        Console.WriteLine("D - Move Down");
        Console.WriteLine("L - Move Left");
        Console.WriteLine("R - Move Right");
        Console.WriteLine("G - Grow Caterpillar");
        Console.WriteLine("S - Shrink Caterpillar");
        Console.WriteLine("Q - Quit");
    }

    private void ProcessCommand(string command)
    {
        switch (command)
        {
            case "U":
            case "D":
            case "L":
            case "R":
                _geca.Move(command[0]);
                _logger.LogMovement($"Moved {command[0]}");
                break;
            case "G":
                _geca.Grow();
                _logger.LogMovement("Grew Caterpillar");
                break;
            case "S":
                _geca.Shrink();
                _logger.LogMovement("Shrank Caterpillar");
                break;
            case "Q":
                Environment.Exit(0);
                break;
            default:
                _logger.LogGeneral($"Invalid command: {command}");
                Console.WriteLine("Invalid command. Please enter U, D, L, R, G, S, or Q.");
                break;
        }

        Console.Clear();
        _geca.Display();
    }
}