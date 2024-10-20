class zad6
{
    static void Main()
    {
        while (true) 
        {
            Console.Write("Wprowadź liczbę całkowitą: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int liczba))
            {
                if (liczba < 0)
                {
                    Console.WriteLine("Wprowadzono liczbę mniejszą od 0");
                    break;
                }

                Console.WriteLine($"Wprowadzona liczba: {liczba}");
            }
            else
            {
                Console.WriteLine("To nie jest poprawna liczba całkowita. Spróbuj ponownie.");
            }
        }
    }
}
