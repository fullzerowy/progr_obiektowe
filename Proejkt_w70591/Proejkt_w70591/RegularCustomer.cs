using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreExample
{
    public class RegularCustomer : Customer
    {
        public RegularCustomer(int id, string name, decimal wallet)
            : base(id, name, wallet)
        {
        }

        public override decimal GetDiscountPercentage() => 0m;
        public override string GetCustomerType() => "RegularCustomer";
    }
}

