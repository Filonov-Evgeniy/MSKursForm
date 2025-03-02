using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class EngineStart
    {
        double timeWork = 0;

        Config config;
        Statistic stats;

        Random random;

        Source source;
        Phase phase1;
        Phase phase2;

        public EngineStart(double u2, double u1)
        {
            config = Config.getInstance();
            stats = new Statistic();
            random = new Random();
            source = new Source();
            phase1 = new Phase(3, u1, config.maxCollectorCapacity);
            phase2 = new Phase(1, u2);
        }

        public Statistic start()
        {
            

            while (timeWork < config.workTime)
            {
                tick();
                if (phase2.channels[0].isReadyToSend())
                {
                    Console.WriteLine($"Заявка {phase2.channels[0].order.id} обработана и выведена из системы");

                    phase2.channels[0].Locked = false;
                    stats.orderDone++;
                }

                if (!phase2.channels[0].isLocked())
                {
                    for (int i = 0; i < phase1.channels.Length; i++)
                    {
                        if (phase1.channels[i].isReadyToSend())
                        {
                            Console.WriteLine($"Заявка {phase1.channels[i].order.id} обработана и передана из канала {i + 1} во вторую фазу");
                            phase2.channels[0].setOrder(phase1.channels[i].sendOrder());
                            phase2.channels[0].normalDistribution();
                            Console.WriteLine($"Время на обработку в фазе 2 {phase2.channels[0].timer}");
                            break;
                        }
                    }
                }

                for (int i = 0; i < phase1.channels.Length; i++)
                {
                    if (!phase1.channels[i].isLocked() && phase1.collector.hasOrder())
                    {
                        Console.WriteLine($"Заявка передана из накопителя в канал {i + 1} фазы 1");
                        phase1.channels[i].setOrder(phase1.collector.sendOrder(stats));
                        phase1.channels[0].exponentialDistribution();
                        Console.WriteLine($"Время на обработку в фазе 1 {phase1.channels[0].timer}");
                    }
                }

                if (source.timer <= 0)
                {
                    Order order = source.sendOrder();

                    if (!phase1.collector.isFull())
                    {
                        phase1.collector.addOrder(order, stats);
                        Console.WriteLine($"Заявка {order.id} передана из источника в накопитель");
                        for (int i = 0; i < phase1.channels.Length; i++)
                        {
                            if (!phase1.channels[i].isLocked())
                            {
                                Console.WriteLine($"Заявка передана из накопителя в канал {i + 1} фазы 1");
                                phase1.channels[i].setOrder(phase1.collector.sendOrder(stats));
                                phase1.channels[0].exponentialDistribution();
                                Console.WriteLine($"Время на обработку в фазе 1 {phase1.channels[0].timer}");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Накопитель заполнен. Заявка {order.id} отклонена");
                        stats.orderLost++;
                    }
                    source.generateOrder();
                    stats.orderGenerate++;
                    Console.WriteLine($"Время до следующей {source.timer}");
                }
            }
            stats.CalculateAbsSystThroughput();
            stats.middleWaitingTime /= stats.fromCollector;
            stats.CalculateMiddleInSyst();
            stats.calculateChanceOfSuccess();

            Console.WriteLine($"\n\nЗаявок принято: {stats.orderGenerate}\n\nЗаявок обработано: {stats.orderDone}\n\nЗаявок отклонено: {stats.orderLost}~{(stats.orderGenerate - stats.orderDone)}\n\nАбсолютная пропускная способность: {stats.absSystemThroughput}\nСреднее время ожидания в очереди: {stats.middleWaitingTime}\nМаксимальное время ожидания в очереди: {stats.maxWaitingTime}\nСреднее кол-во заявок в системе: {stats.middleOrderInSystem}");
            return stats;

        }
        public void tick()
        {
            timeWork += config.updateTime;

            phase1.denyTime(stats);
            phase2.denyTime(stats);

            source.timer -= 1;
            phase2.channels[0].timer -= config.updateTime;
            phase1.collector.tick(config.updateTime);

            for (int i = 0; i < phase1.channels.Length; i++)
            {
                phase1.channels[i].timer -= config.updateTime;
            }

            if (phase1.collector.isDenyTime(stats))
            {
                stats.orderLost++;
            }
            stats.checkOrdersInSyst([phase1, phase2]);
        }
    }
}
