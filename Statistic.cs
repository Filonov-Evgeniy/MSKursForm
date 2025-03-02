using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Statistic
    {
        Config config = Config.getInstance();

        public double absSystemThroughput = 0; //Абсолютная пропускная способность системы
        public double middleWaitingTime = 0; //Среднее время ожидания в очереди
        public double maxWaitingTime = 0; //Максимальное время ожидания в очереди
        public double middleOrderInSystem = 0; //Среднее число заявок в системе
        public double orderDone = 0; //Заявок выполнено
        public double orderLost = 0; //Заявок потеряно
        public double orderGenerate = 0; //Заявок сгенерировано, находящееся в системе

        public int fromCollector = 0;
        public int ticsCount = 0;
        int ordersInSyst = 0;

        public double chanceOfSuccess = 0; 

        public void calculateChanceOfSuccess()
        {
            this.chanceOfSuccess = orderDone / orderGenerate;
        }

        public void CalculateAbsSystThroughput()
        {
            double p1SystThroughput = 0;
            if (config.lambda > 3 * config.u1)
            {
                p1SystThroughput = config.u1;
            }
            else
            {
                p1SystThroughput = config.lambda;
            }

            if (p1SystThroughput > 3 * config.u2)
            {
                absSystemThroughput = config.u2;
            }
            else
            {
                absSystemThroughput = p1SystThroughput;
            }
        }

        public void CalculateMiddleInSyst()
        {
            middleOrderInSystem = (double)ordersInSyst / (double)ticsCount;
        }

        public void checkOrdersInSyst(Phase[] phases)
        {
            ticsCount++;
            foreach (Phase phase in phases)
            {
                for (int i = 0; i < phase.channels.Length; i++)
                {
                    if (phase.channels[i].isLocked())
                    {
                        ordersInSyst++;
                    }
                }
                if (phase.collector.maxQueue != 0)
                {
                    ordersInSyst += phase.collector.getCount();
                }
            }
        }
    }
}
