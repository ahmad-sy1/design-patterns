namespace GameApp
{
    public class GameFacade
    {
        private readonly GraphicsSystem graphics;
        private readonly AudioSystem audio;
        private readonly SaveSystem saveSystem;
        private readonly NetworkService network;
        private readonly GameEngine gameEngine;
        private readonly DebugConsole debugConsole;

        private GameMode mode;
        private ConnectionMode connection;

        public GameFacade(GraphicsSystem graphics, AudioSystem audio, SaveSystem saveSystem,
            NetworkService network, GameEngine gameEngine, DebugConsole debugConsole)
        {
            this.graphics = graphics;
            this.audio = audio;
            this.saveSystem = saveSystem;
            this.network = network;
            this.gameEngine = gameEngine;
            this.debugConsole = debugConsole;
        }

        public void StartGame(GameMode mode, ConnectionMode connection)
        {
            this.mode = mode;
            this.connection = connection;

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

            if (connection == ConnectionMode.Online)
            {
                network.Connect();
                network.Login();
            }

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

            if (connection == ConnectionMode.Online)
            {
                network.Logout();
                network.Disconnect();
            }

            if (mode == GameMode.Developer)
            {
                debugConsole.Close();
            }

            audio.Shutdown();
            graphics.Shutdown();
        }
    }
}
