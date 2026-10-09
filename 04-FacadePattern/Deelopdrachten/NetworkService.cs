namespace GameApp
{
    public class NetworkService
    {
        public void Connect() { Console.WriteLine("Verbonden met gameserver."); }
        public void Login() { Console.WriteLine("Speler ingelogd."); }
        public void Logout() { Console.WriteLine("Speler uitgelogd."); }
        public void Disconnect() { Console.WriteLine("Verbinding met gameserver verbroken."); }
    }
}
