using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class BarberShop
    {
        private readonly Semaphore waitingRoom = new Semaphore(3, 3);
        private readonly Semaphore barberSleep = new Semaphore(0, 1);
        private readonly Semaphore barberChair = new Semaphore(1, 1);
        private readonly Semaphore customerReady = new Semaphore(0, 1);

        private bool isSleeping = true;
        private int customerCount = 0;
        private bool done = true;

        public void RunSimulation()
        {
            Thread barberThread = new Thread(Barber);
            barberThread.Start();

            Thread[] customerThreads = new Thread[10];

            for (int i = 0; i < customerThreads.Length; i++)
            {
                customerThreads[i] = new Thread(new ParameterizedThreadStart(Customer));
                customerThreads[i].Name = $"Visitor {i + 1}";
                customerThreads[i].Start(i + 1);
                Thread.Sleep(1000);
            }

            foreach (Thread thread in customerThreads)
            {
                thread.Join();
            }
            done = false;
            Thread.Sleep(3000);
            Console.WriteLine("All customers have been served.");
            barberThread.Join();
            Console.WriteLine("End of simulation!");
        }

        private void Barber()
        {
            while (done)
            {
                if (customerCount > 0)
                {
                    Console.WriteLine("Barber starts cutting...");
                    Thread.Sleep(2000);
                    Console.WriteLine($"Barber finishes cutting.");
                    customerReady.Release();
                }
                else
                {
                    Console.WriteLine("Barber is sleeping...");
                    isSleeping = true;
                    barberSleep.WaitOne();
                }
            }
        }

        private void Customer(object visitorId)
        {
            Console.WriteLine($"Visitor {visitorId} entered the barber shop.");

            if (waitingRoom.WaitOne(0))
            {
                customerCount++;
                Console.WriteLine($"Visitor {visitorId} in meeting room");

                barberChair.WaitOne();
                waitingRoom.Release();

                if (isSleeping)
                {
                    barberSleep.Release();
                    Console.WriteLine($"Woke up the barber for visitor {visitorId}");
                    isSleeping = false;
                }

                customerReady.WaitOne();
                barberChair.Release();
                Console.WriteLine($"Visitor {visitorId} leaves.");
                customerCount--;
            }
            else
            {
                Console.WriteLine($"Visitor {visitorId}: No available seats. Leaving...");
            }
        }
    }
}
