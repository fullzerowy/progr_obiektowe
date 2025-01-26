using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputerStoreExample
{
    public class ShopManager
    {
        private const string CustomersFilePath = "customers.txt";
        private const string ProductsFilePath = "products.txt";

        public List<Customer> Customers { get; private set; }
        public List<Product> Products { get; private set; }

        public ShopManager()
        {
            Customers = new List<Customer>();
            Products = new List<Product>();
            LoadCustomersFromFile();
            LoadProductsFromFile();
        }

        public void AddCustomer(Customer customer)
        {
            if (Customers.Any(c => c.Id == customer.Id))
                throw new Exception("Klient o takim ID już istnieje.");
            Customers.Add(customer);
            SaveCustomersToFile();
        }

        public Customer GetCustomerById(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            var existing = GetCustomerById(updatedCustomer.Id);
            if (existing != null)
            {
                existing.Name = updatedCustomer.Name;
                existing.Wallet = updatedCustomer.Wallet;
                SaveCustomersToFile();
            }
        }

        public void DeleteCustomer(int id)
        {
            var toRemove = GetCustomerById(id);
            if (toRemove != null)
            {
                Customers.Remove(toRemove);
                SaveCustomersToFile();
            }
        }

        public void AddProduct(Product product)
        {
            if (Products.Any(p => p.Id == product.Id))
                throw new Exception("Produkt o takim ID już istnieje.");
            Products.Add(product);
            SaveProductsToFile();
        }

        public Product GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var existing = GetProductById(updatedProduct.Id);
            if (existing != null)
            {
                existing.Name = updatedProduct.Name;
                existing.Price = updatedProduct.Price;
                existing.Quantity = updatedProduct.Quantity;
                SaveProductsToFile();
            }
        }

        public void DeleteProduct(int id)
        {
            var toRemove = GetProductById(id);
            if (toRemove != null)
            {
                Products.Remove(toRemove);
                SaveProductsToFile();
            }
        }

        public void RestockProduct(int productId, int addQuantity)
        {
            var product = GetProductById(productId);
            if (product != null && addQuantity > 0)
            {
                product.Quantity += addQuantity;
                SaveProductsToFile();
            }
        }

        public void SellProducts(int customerId, List<(int productId, int quantity)> cart)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Nie znaleziono klienta o takim ID.");
                return;
            }

            decimal totalPrice = 0m;
            foreach (var (productId, quantity) in cart)
            {
                var product = GetProductById(productId);
                if (product == null)
                {
                    Console.WriteLine($"Nie znaleziono produktu o ID {productId}. Pomijam...");
                    continue;
                }
                if (quantity > product.Quantity)
                {
                    Console.WriteLine($"Brak wystarczającej liczby '{product.Name}'. Na stanie {product.Quantity}, potrzebne {quantity}. Pomijam...");
                    continue;
                }
                decimal itemCost = product.Price * quantity;
                totalPrice += itemCost;
            }

            decimal discountPercentage = customer.GetDiscountPercentage();
            decimal discountValue = totalPrice * discountPercentage;
            decimal finalPrice = totalPrice - discountValue;

            Console.WriteLine($"Cena przed rabatem: {totalPrice}");
            if (discountPercentage > 0)
            {
                Console.WriteLine($"Rabat {discountPercentage * 100}%: -{discountValue}");
                Console.WriteLine($"Cena po rabacie: {finalPrice}");
            }

            if (finalPrice > customer.Wallet)
            {
                Console.WriteLine($"Za mało środków! Klient {customer.Name} ma {customer.Wallet}, potrzeba {finalPrice}.");
                return;
            }

            customer.Wallet -= finalPrice;
            foreach (var (productId, quantity) in cart)
            {
                var product = GetProductById(productId);
                if (product != null && quantity <= product.Quantity)
                {
                    product.Quantity -= quantity;
                }
            }

            Console.WriteLine("Zakup udany!");
            SaveCustomersToFile();
            SaveProductsToFile();
        }

        private void LoadCustomersFromFile()
        {
            if (!File.Exists(CustomersFilePath))
                return;

            var lines = File.ReadAllLines(CustomersFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 4) continue;

                int id = int.Parse(parts[0]);
                string name = parts[1];
                decimal wallet = decimal.Parse(parts[2]);
                string type = parts[3];

                switch (type)
                {
                    case "RegularCustomer":
                        Customers.Add(new RegularCustomer(id, name, wallet));
                        break;
                    case "WholesaleCustomer":
                        Customers.Add(new WholesaleCustomer(id, name, wallet));
                        break;
                }
            }
        }

        private void SaveCustomersToFile()
        {
            var lines = new List<string>();
            foreach (var c in Customers)
                lines.Add(c.ToString());
            File.WriteAllLines(CustomersFilePath, lines);
        }

        private void LoadProductsFromFile()
        {
            if (!File.Exists(ProductsFilePath))
                return;

            var lines = File.ReadAllLines(ProductsFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 5) continue;

                int id = int.Parse(parts[0]);
                string name = parts[1];
                decimal price = decimal.Parse(parts[2]);
                int quantity = int.Parse(parts[3]);
                string type = parts[4];

                switch (type)
                {
                    case "Laptop":
                        Products.Add(new Laptop(id, name, price, quantity));
                        break;
                    case "Desktop":
                        Products.Add(new Desktop(id, name, price, quantity));
                        break;
                    case "Accessory":
                        Products.Add(new Accessory(id, name, price, quantity));
                        break;
                }
            }
        }

        private void SaveProductsToFile()
        {
            var lines = new List<string>();
            foreach (var p in Products)
                lines.Add(p.ToString());
            File.WriteAllLines(ProductsFilePath, lines);
        }
    }
}

