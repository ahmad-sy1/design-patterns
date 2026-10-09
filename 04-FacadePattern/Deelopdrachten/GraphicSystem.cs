namespace GameApp
{
    public class GraphicsSystem
    {
        public void Initialize() { Console.WriteLine("Graphics geïnitialiseerd."); }
        public void SetResolution(int width, int height) { Console.WriteLine($"Resolutie ingesteld op {width}x{height}."); }
        public void Shutdown() { Console.WriteLine("Graphics afgesloten."); }
    }
}
