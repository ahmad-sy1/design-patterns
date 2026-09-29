using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern.Interfaces
{
    internal class TurkeyAdapter : Duck
    {
        private Turkey turkey;
        public TurkeyAdapter(Turkey turkey) 
        {
            this.turkey = turkey;
        }
        public void Quack()
        {
            turkey.Gobble();
        }

        public void Fly()
        {
            for (int i = 0; i < 5; i++)
            {
                turkey.Fly();
            }
        }
    }
}
