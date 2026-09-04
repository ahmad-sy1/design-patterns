namespace StrategyPattern.Interfaces.QuackBehavior
{

    public class MuteQuack: QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("<<Silence>>");
        }
    }
}