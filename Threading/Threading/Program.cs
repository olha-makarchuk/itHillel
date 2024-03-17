using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            BarberShop barberShop = new BarberShop();
            barberShop.RunSimulation();
        }
    }
}
