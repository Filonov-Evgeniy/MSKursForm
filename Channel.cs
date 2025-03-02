using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Channel
    {
        public double timer = 0;
        public Order order;
        private bool locked;
        private double u;

        Config config = Config.getInstance();
        Random random = new Random();

        public Channel(double u)
        {
            locked = false;
            this.u = u;
        }

        public bool Locked
        {
            set { locked = value; }
        }

        public bool isLocked()
        {
            return locked;
        }

        public void setOrder(Order order)
        {
            this.order = order;
            this.locked = true;
        }

        public Order sendOrder()
        {
            this.locked = false;
            return this.order;
        }

        public bool isReadyToSend()
        {
            if (this.timer <= 0 && locked)
            {
                return true;
            }
            return false;
        }

        public void exponentialDistribution()
        {
            double t = -1;
            while (t < 0)
            {
                double U = random.NextDouble();
                t = -1 / u * Math.Log(U);
            }
            timer = t;
        }

        public void normalDistribution()
        {
            double t = -1;
            while (t < 0)
            {
                double x = random.NextDouble();
                double a = 1 / u - config.omega * Math.Sqrt(3);
                double b = 1 / u + config.omega * Math.Sqrt(3);
                t = (b - a) * x + a;
            }
            timer = t;
        }
    }
}
