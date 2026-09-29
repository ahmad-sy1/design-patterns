using AdapterPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern.Turkeys
{
    internal class WildTurkey : Turkey
    {
        public void Fly()
        {
            Console.WriteLine("Gobble gobble");
        }

        public void Gobble()
        {
            Console.WriteLine("I'm flying a short distance");
        }
    }
}
