namespace GameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();

            Console.WriteLine("=== NormalMode starten ===");
            game.StartGame(GameMode.Normal);

            Console.WriteLine();
            Console.WriteLine("=== NormalMode stoppen ===");
            game.StopGame();

            Console.WriteLine();
            Console.WriteLine("=== DeveloperMode starten ===");
            game.StartGame(GameMode.Developer);

            Console.WriteLine();
            Console.WriteLine("=== DeveloperMode stoppen ===");
            game.StopGame();

            Console.ReadLine();
        }
    }
}
