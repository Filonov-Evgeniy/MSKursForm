using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSKursForms
{
    class Order
    {
        public int id = 0;
        public double timeInSystem = 0;
        public double timeInCollector = 0;
        public double waitingInOrder = 0;

        public Order(int id)
        {
            this.id = id;
        }
    }
}
