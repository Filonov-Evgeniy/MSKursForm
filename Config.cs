using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    public class Config
    {
        public double u1 = 2;
        public double u2 = 1;
        public double u2Final = 10;
        public double step = 0.5;

        public int maxCollectorCapacity = 15;

        public double lambda = 5;

        public double updateTime = 0.2;
        public double dt = 5;
        public double workTime = 1000;

        public double omega = 1;

        public double experimentU1 = 3;
        public double experimentU2 = 5;
        public double experimentDelta1 = 1.5;
        public double experimentDelta2 = 2;

        private static Config instance;

        private Config() { }

        public static Config getInstance()
        {
            if (instance == null)
            {
                instance = new Config();
            }
            return instance;
        }
    }
}
