using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreExample
{
    public abstract class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Wallet { get; set; }

        protected Customer(int id, string name, decimal wallet)
        {
            Id = id;
            Name = name;
            Wallet = wallet;
        }

        public abstract decimal GetDiscountPercentage();
        public abstract string GetCustomerType();

        public override string ToString()
        {
            return $"{Id},{Name},{Wallet},{GetCustomerType()}";
        }
    }
}

