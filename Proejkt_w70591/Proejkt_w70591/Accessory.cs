using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreExample
{
    public class Accessory : Product
    {
        public Accessory(int id, string name, decimal price, int quantity)
            : base(id, name, price, quantity)
        {
        }

        public override string GetProductType() => "Accessory";
    }
}
