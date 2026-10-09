namespace GameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameFacade game = new GameFacade(
                new GraphicsSystem(),
                new AudioSystem(),
                new SaveSystem(),
                new NetworkService(),
                new GameEngine(),
                new DebugConsole());

            Console.WriteLine("=== NormalMode online starten ===");
            game.StartGame(GameMode.Normal, ConnectionMode.Online);

            Console.WriteLine();
            Console.WriteLine("=== NormalMode online stoppen ===");
            game.StopGame();

            Console.WriteLine();
            Console.WriteLine("=== NormalMode offline starten ===");
            game.StartGame(GameMode.Normal, ConnectionMode.Offline);

            Console.WriteLine();
            Console.WriteLine("=== NormalMode offline stoppen ===");
            game.StopGame();

            Console.WriteLine();
            Console.WriteLine("=== DeveloperMode offline starten ===");
            game.StartGame(GameMode.Developer, ConnectionMode.Offline);

            Console.WriteLine();
            Console.WriteLine("=== DeveloperMode offline stoppen ===");
            game.StopGame();

            Console.ReadLine();
        }
    }
}
