using System;
using System.Collections.Generic;

namespace ComputerStoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopManager shop = new ShopManager();

            while (true)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine(" SKLEP KOMPUTEROWY - MENU GŁÓWNE ");
                Console.WriteLine("=========================================");
                Console.WriteLine(" 1. Dodaj klienta");
                Console.WriteLine(" 2. Lista klientów");
                Console.WriteLine(" 3. Edycja klienta");
                Console.WriteLine(" 4. Usuń klienta");
                Console.WriteLine(" 5. Dodaj produkt");
                Console.WriteLine(" 6. Lista produktów");
                Console.WriteLine(" 7. Edycja produktu");
                Console.WriteLine(" 8. Usuń produkt");
                Console.WriteLine(" 9. Przyjęcie towaru");
                Console.WriteLine("10. Sprzedaż (koszyk)");
                Console.WriteLine(" 0. Wyjście");
                Console.WriteLine("=========================================");
                Console.Write("Wybierz opcję: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Błędny wybór. Naciśnij Enter...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        AddCustomerMenu(shop);
                        break;
                    case 2:
                        ListCustomers(shop);
                        break;
                    case 3:
                        UpdateCustomerMenu(shop);
                        break;
                    case 4:
                        DeleteCustomerMenu(shop);
                        break;
                    case 5:
                        AddProductMenu(shop);
                        break;
                    case 6:
                        ListProducts(shop);
                        break;
                    case 7:
                        UpdateProductMenu(shop);
                        break;
                    case 8:
                        DeleteProductMenu(shop);
                        break;
                    case 9:
                        RestockMenu(shop);
                        break;
                    case 10:
                        SellMenu(shop);
                        break;
                    default:
                        Console.WriteLine("Błędny wybór. Naciśnij Enter...");
                        break;
                }

                Console.WriteLine("Naciśnij Enter, aby kontynuować...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void AddCustomerMenu(ShopManager shop)
        {
            Console.WriteLine("Dodawanie klienta");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Imię: ");
            string name = Console.ReadLine();
            Console.Write("Stan portfela: ");
            decimal wallet = decimal.Parse(Console.ReadLine());

            Console.Write("Typ klienta (1-hurtowy / 2-zwykły): ");
            string type = Console.ReadLine();

            try
            {
                if (type == "1")
                {
                    shop.AddCustomer(new WholesaleCustomer(id, name, wallet));
                    Console.WriteLine("Klient hurtowy dodany.");
                }
                else
                {
                    shop.AddCustomer(new RegularCustomer(id, name, wallet));
                    Console.WriteLine("Klient zwykły dodany.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }

        static void ListCustomers(ShopManager shop)
        {
            Console.WriteLine("=== Lista klientów ===");
            foreach (var c in shop.Customers)
                Console.WriteLine($"ID={c.Id}, Imię={c.Name}, Portfel={c.Wallet}, Typ={c.GetCustomerType()}");
        }

        static void UpdateCustomerMenu(ShopManager shop)
        {
            Console.Write("ID klienta do edycji: ");
            int id = int.Parse(Console.ReadLine());
            var cust = shop.GetCustomerById(id);
            if (cust == null)
            {
                Console.WriteLine("Nie znaleziono klienta.");
                return;
            }

            Console.Write($"Nowe imię (było {cust.Name}): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) cust.Name = newName;

            Console.Write($"Nowy stan portfela (było {cust.Wallet}): ");
            string walletStr = Console.ReadLine();
            if (decimal.TryParse(walletStr, out decimal w))
                cust.Wallet = w;

            shop.UpdateCustomer(cust);
            Console.WriteLine("Klient zaktualizowany.");
        }

        static void DeleteCustomerMenu(ShopManager shop)
        {
            Console.Write("ID klienta do usunięcia: ");
            int id = int.Parse(Console.ReadLine());
            var c = shop.GetCustomerById(id);
            if (c == null)
            {
                Console.WriteLine("Nie znaleziono klienta.");
                return;
            }

            shop.DeleteCustomer(id);
            Console.WriteLine("Klient usunięty.");
        }

        static void AddProductMenu(ShopManager shop)
        {
            Console.WriteLine("Dodawanie produktu");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nazwa: ");
            string name = Console.ReadLine();
            Console.Write("Cena: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Ilość początkowa: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Typ produktu: ");
            Console.WriteLine("1 - Laptop");
            Console.WriteLine("2 - Desktop");
            Console.WriteLine("3 - Accessory");
            string type = Console.ReadLine();

            try
            {
                Product product;
                switch (type)
                {
                    case "1":
                        product = new Laptop(id, name, price, quantity);
                        break;
                    case "2":
                        product = new Desktop(id, name, price, quantity);
                        break;
                    default:
                        product = new Accessory(id, name, price, quantity);
                        break;
                }
                shop.AddProduct(product);
                Console.WriteLine("Produkt dodany.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }

        static void ListProducts(ShopManager shop)
        {
            Console.WriteLine("=== Lista produktów ===");
            foreach (var p in shop.Products)
                Console.WriteLine($"ID={p.Id}, Nazwa={p.Name}, Cena={p.Price}, Ilość={p.Quantity}, Typ={p.GetProductType()}");
        }

        static void UpdateProductMenu(ShopManager shop)
        {
            Console.Write("ID produktu do edycji: ");
            int id = int.Parse(Console.ReadLine());
            var product = shop.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine("Nie znaleziono produktu.");
                return;
            }

            Console.Write($"Nowa nazwa (była {product.Name}): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;

            Console.Write($"Nowa cena (była {product.Price}): ");
            string priceStr = Console.ReadLine();
            if (decimal.TryParse(priceStr, out decimal newPrice))
                product.Price = newPrice;

            Console.Write($"Nowa ilość (było {product.Quantity}): ");
            string qtyStr = Console.ReadLine();
            if (int.TryParse(qtyStr, out int newQty))
                product.Quantity = newQty;

            shop.UpdateProduct(product);
            Console.WriteLine("Produkt zaktualizowany.");
        }

        static void DeleteProductMenu(ShopManager shop)
        {
            Console.Write("ID produktu do usunięcia: ");
            int id = int.Parse(Console.ReadLine());
            var p = shop.GetProductById(id);
            if (p == null)
            {
                Console.WriteLine("Nie znaleziono produktu.");
                return;
            }

            shop.DeleteProduct(id);
            Console.WriteLine("Produkt usunięty.");
        }

        static void RestockMenu(ShopManager shop)
        {
            Console.Write("ID produktu do przyjęcia: ");
            int id = int.Parse(Console.ReadLine());
            var product = shop.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine("Nie znaleziono produktu.");
                return;
            }

            Console.Write("O ile zwiększyć stan? ");
            int qty = int.Parse(Console.ReadLine());
            if (qty <= 0)
            {
                Console.WriteLine("Ilość musi być dodatnia.");
                return;
            }

            shop.RestockProduct(id, qty);
            Console.WriteLine("Stan produktu został zaktualizowany.");
        }

        static void SellMenu(ShopManager shop)
        {
            Console.Write("ID klienta: ");
            int customerId = int.Parse(Console.ReadLine());
            var customer = shop.GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Nie znaleziono klienta.");
                return;
            }

            Console.WriteLine($"Klient: {customer.Name}, typ: {customer.GetCustomerType()}, portfel: {customer.Wallet}");
            Console.WriteLine("Tworzenie koszyka. Wpisz ID produktu i ilość. Pusta linia kończy koszyk.");

            var cart = new List<(int productId, int quantity)>();
            while (true)
            {
                Console.Write("ID produktu (Enter = koniec): ");
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;

                if (!int.TryParse(line, out int productId))
                {
                    Console.WriteLine("Błędny input. Pomijam.");
                    continue;
                }

                var product = shop.GetProductById(productId);
                if (product == null)
                {
                    Console.WriteLine("Nie znaleziono produktu. Pomijam.");
                    continue;
                }

                Console.Write($"Ilość (max {product.Quantity}): ");
                if (!int.TryParse(Console.ReadLine(), out int qty))
                {
                    Console.WriteLine("Błędny input. Pomijam.");
                    continue;
                }

                if (qty <= 0)
                {
                    Console.WriteLine("Ilość musi być dodatnia. Pomijam.");
                    continue;
                }

                cart.Add((productId, qty));
            }

            shop.SellProducts(customerId, cart);
        }
    }
}
