using AdapterPattern.Interfaces;
using System;

namespace AdapterPattern.Geese
{
    internal class CanadaGoose : Goose
    {
        public void Fly()
        {
            Console.WriteLine("I'm flying a long distance in V-formation");
        }

        public void Honk()
        {
            Console.WriteLine("Honk honk");
        }
    }
}
