namespace StrategyPattern.Interfaces.SwimBehavior
{
    public class SinkSwim : SwimBehavior
    {
        public void Swim()
        {
            Console.WriteLine("I'm sinking!");
        }
    }
}