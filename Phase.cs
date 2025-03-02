using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Phase
    {
        Config config = Config.getInstance();

        public Collector collector;
        public Channel[] channels;

        public Phase(int channelsCount, double u, int collectorCapacity = 0)
        {
            this.channels = new Channel[channelsCount];
            this.collector = new Collector(collectorCapacity);

            for (int i = 0; i < channelsCount; i++)
            {
                this.channels[i] = new Channel(u);
            }
        }

        public void denyTime(Statistic stats)
        {
            foreach (Channel channel in channels)
            {
                if (channel.isLocked() && channel.order.timeInSystem > config.dt)
                {
                    channel.Locked = false;
                    stats.orderLost++;
                    Console.WriteLine($"Время заявки {channel.order.id} истекло. Заявка выведена из системы.//////////////////////");
                }
            }
        }
    }
}
