namespace GameApp
{
    public class GameManager
    {
        private readonly GraphicsSystem graphics = new GraphicsSystem();
        private readonly AudioSystem audio = new AudioSystem();
        private readonly SaveSystem saveSystem = new SaveSystem();
        private readonly NetworkService network = new NetworkService();
        private readonly GameEngine gameEngine = new GameEngine();
        private readonly DebugConsole debugConsole = new DebugConsole();

        private GameMode mode;

        public void StartGame(GameMode mode)
        {
            this.mode = mode;

            graphics.Initialize();
            if (mode == GameMode.Developer)
            {
                graphics.SetResolution(1280, 720);
            }
            else
            {
                graphics.SetResolution(1920, 1080);
            }

            audio.Initialize();
            audio.SetVolume(mode == GameMode.Developer ? 0 : 70);

            saveSystem.LoadSettings();
            saveSystem.LoadPlayer();

            network.Connect();
            network.Login();

            if (mode == GameMode.Developer)
            {
                debugConsole.Open();
                debugConsole.ShowFps();
            }

            gameEngine.LoadWorld();
            gameEngine.Start();
        }

        public void StopGame()
        {
            gameEngine.Stop();
            gameEngine.UnloadWorld();

            if (mode == GameMode.Normal)
            {
                saveSystem.SavePlayer();
            }
            saveSystem.SaveSettings();

            network.Logout();
            network.Disconnect();

            if (mode == GameMode.Developer)
            {
                debugConsole.Close();
            }

            audio.Shutdown();
            graphics.Shutdown();
        }
    }
}
