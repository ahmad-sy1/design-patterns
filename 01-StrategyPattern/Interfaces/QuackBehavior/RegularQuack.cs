namespace StrategyPattern.Interfaces.QuackBehavior
{

    public class RegularQuack : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Quack");
        }

    }
}