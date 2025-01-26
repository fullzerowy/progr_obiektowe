using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreExample
{
    public class WholesaleCustomer : Customer
    {
        public WholesaleCustomer(int id, string name, decimal wallet)
            : base(id, name, wallet)
        {
        }

        public override decimal GetDiscountPercentage() => 0.10m;
        public override string GetCustomerType() => "WholesaleCustomer";
    }
}
