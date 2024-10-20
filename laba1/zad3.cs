class zad3
{
    static void Main()
    {
        double[] liczby = new double[10];

        for (int i = 0; i < liczby.Length; i++)
        {
            Console.WriteLine($"Podaj liczbę rzeczywistą nr {i + 1}: ");
            liczby[i] = Convert.ToDouble(Console.ReadLine());
        }

        Console.WriteLine("\nTablica od pierwszego do ostatniego indeksu:");
        for (int i = 0; i < liczby.Length; i++)
        {
            Console.WriteLine(liczby[i]);
        }

        Console.WriteLine("\nTablica od ostatniego do pierwszego indeksu:");
        for (int i = liczby.Length - 1; i >= 0; i--)
        {
            Console.WriteLine(liczby[i]);
        }

        Console.WriteLine("\nElementy o nieparzystych indeksach:");
        for (int i = 1; i < liczby.Length; i += 2)
        {
            Console.WriteLine(liczby[i]);
        }

        Console.WriteLine("\nElementy o parzystych indeksach:");
        for (int i = 0; i < liczby.Length; i += 2)
        {
            Console.WriteLine(liczby[i]);
        }
    }
}
