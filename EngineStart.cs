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

        Source source;
        Phase phase1;
        Phase phase2;

        public EngineStart(double u2, double u1)
        {
            config = Config.getInstance();
            stats = new Statistic();
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
                    phase2.channels[0].Locked = false;
                    stats.orderDone++;
                }

                if (!phase2.channels[0].isLocked())
                {
                    for (int i = 0; i < phase1.channels.Length; i++)
                    {
                        if (phase1.channels[i].isReadyToSend())
                        {
                            phase2.channels[0].setOrder(phase1.channels[i].sendOrder());
                            phase2.channels[0].normalDistribution();
                            break;
                        }
                    }
                }

                for (int i = 0; i < phase1.channels.Length; i++)
                {
                    if (!phase1.channels[i].isLocked() && phase1.collector.hasOrder())
                    {
                        phase1.channels[i].setOrder(phase1.collector.sendOrder(stats));
                        phase1.channels[0].exponentialDistribution();
                    }
                }

                if (source.timer <= 0)
                {
                    Order order = source.sendOrder();

                    if (!phase1.collector.isFull())
                    {
                        phase1.collector.addOrder(order, stats);
                        for (int i = 0; i < phase1.channels.Length; i++)
                        {
                            if (!phase1.channels[i].isLocked())
                            {
                                phase1.channels[i].setOrder(phase1.collector.sendOrder(stats));
                                phase1.channels[0].exponentialDistribution();
                                break;
                            }
                        }
                    }
                    else
                    {
                        stats.orderLost++;
                    }
                    source.generateOrder();
                    stats.orderGenerate++;
                }
            }
            stats.CalculateAbsSystThroughput();
            stats.middleWaitingTime /= stats.fromCollector;
            stats.CalculateMiddleInSyst();
            stats.calculateChanceOfSuccess();
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
