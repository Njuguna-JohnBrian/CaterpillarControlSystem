namespace CaterpillarControlSystem.control
{
    internal abstract class Program
    {
        private static void Main()
        {
            var controlSystem = new Operations.CaterpillarControlSystem();
            controlSystem.Run();
        }
    }
}