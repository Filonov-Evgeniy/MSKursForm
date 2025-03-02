using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Source
    {
        public double timer = 0;
        public int orderCount = 1;
        public Config config = Config.getInstance();
        private Random random = new Random();

        //public void generateOrder()
        //{
        //    double U = random.NextDouble();
        //    double T = -1 / config.lambda * Math.Log(U);
        //    timer = T;
        //}
        public void generateOrder()
        {
            double t = -1;
            while (t < 0)
            {
                double x = random.NextDouble();
                double a = 1 / config.lambda - config.omega * Math.Sqrt(3);
                double b = 1 / config.lambda + config.omega * Math.Sqrt(3);
                t = (b - a) * x + a;
            }
            timer = t;
        }

        public Order sendOrder()
        {
            Order order = new Order(orderCount);
            orderCount++;
            return order;
        }
    }
}
