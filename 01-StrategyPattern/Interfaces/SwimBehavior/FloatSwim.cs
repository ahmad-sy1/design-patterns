
namespace StrategyPattern.Interfaces.SwimBehavior
{
    public class FloatSwim : SwimBehavior
    {
        public void Swim()
        {
            Console.WriteLine("All ducks float, even decoys!");
        }
    }
}
