namespace GameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GraphicsSystem graphics = new GraphicsSystem();
            AudioSystem audio = new AudioSystem();
            SaveSystem saveSystem = new SaveSystem();
            NetworkService network = new NetworkService();
            GameEngine gameEngine = new GameEngine();

            graphics.Initialize();
            graphics.SetResolution(1920, 1080);

            audio.Initialize();
            audio.SetVolume(70);

            saveSystem.LoadSettings();
            saveSystem.LoadPlayer();

            network.Connect();
            network.Login();

            gameEngine.LoadWorld();
            gameEngine.Start();

            Console.ReadLine();
        }
    }
}
