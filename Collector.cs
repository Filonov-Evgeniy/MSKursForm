using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Collector
    {
        private Queue<Order> waitingQueue;
        private Config config = Config.getInstance();

        public int maxQueue = 0;

        public Collector(int maxQueue)
        {
            waitingQueue = new Queue<Order>(maxQueue);
            this.maxQueue = maxQueue;
        }

        public bool isFull()
        {
            if (waitingQueue.Count < config.maxCollectorCapacity)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool hasOrder()
        {
            if (waitingQueue.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void addOrder(Order order, Statistic stats)
        {
            stats.fromCollector++;
            waitingQueue.Enqueue(order);
        }

        public Order sendOrder(Statistic stats)
        {
            stats.middleWaitingTime += waitingQueue.Peek().timeInCollector;
            if (stats.maxWaitingTime < waitingQueue.Peek().timeInCollector)
            {
                stats.maxWaitingTime = waitingQueue.Peek().timeInCollector;
            }
            return waitingQueue.Dequeue();
        }

        public void tick(double time)
        {
            foreach (Order order in waitingQueue)
            {
                order.timeInCollector += time;
                order.timeInSystem += time;
            }
        }

        public bool isDenyTime(Statistic stats)
        {
            if (hasOrder())
            {
                if (waitingQueue.Peek().timeInSystem > config.dt)
                {
                    stats.middleWaitingTime += waitingQueue.Peek().timeInCollector;
                    if (stats.maxWaitingTime < waitingQueue.Peek().timeInCollector)
                    {
                        stats.maxWaitingTime = waitingQueue.Peek().timeInCollector;
                    }
                    Console.WriteLine($"Срок действия заявки {waitingQueue.Dequeue().id} истек. Заявка покидает систему.");
                    return true;
                }
            }
            return false;
        }

        public int getCount()
        {
            return waitingQueue.Count();
        }
    }
}
