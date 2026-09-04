namespace StrategyPattern.Interfaces.FlyBehavior;

public class FlyWithWings : FlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("I'm flying using my wings!");
    }
}